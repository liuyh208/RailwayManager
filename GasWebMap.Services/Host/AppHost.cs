using Funq;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using GasWebMap.Domain;
using GasWebMap.Repository.MySql;
using GasWebMap.Services.Responses;
using ServiceStack;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;

namespace GasWebMap.Services.Host
{
    public class ServiceInit
    {
        private ServiceInit()
        {
        }

        private static AppHost appHost;

        public static  void Init()
        {
            AppEx.Start();
            
            var autofac = AppEx.Container as AutofacContainer;
            autofac.RegisterGeneric(typeof (IRepository<,>), typeof (Repository<,>));
            autofac.RegisterGeneric(typeof (IRepository<>), typeof (Repository<>));
            appHost = new AppHost();
            appHost.Init();

            var tm = autofac.GetInstance<ITableManager>();

            tm.CreateTableWithSameNameSpace<User>(false);

            //InitData.Init();
            MapperHelper.InitMapper();
        }

        public static string IsLogin()
        {
            return appHost.IsLogin();
        }

        public static void Logout()
        {
             appHost.Logout();
        }

        public static bool HasRole(string role)
        {
            return  appHost.HasRole(role);
        }
    }

    public class AppHost : AppHostBase
    {
        //Tell Service Stack the name of your application and where to find your web services
        public AppHost()
            : base("GasMap Web Services", typeof (AccountService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            SetConfig(new EndpointHostConfig
            {
                DefaultContentType = ContentType.Json,
                GlobalResponseHeaders =
                {
                    {"Access-Control-Allow-Origin", "*"},
                    {"Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS"},
                    {"Access-Control-Allow-Headers","Conteent-Type"}
                },
            });

            // register storage for user sessions 
            container.Register<ICacheClient>(new MemoryCacheClient());
            container.Register<ISessionFactory>(c => new SessionFactory(c.Resolve<ICacheClient>()));

            // Register AuthFeature with custom user session and custom auth provider
            Plugins.Add(new AuthFeature(
                () => new CustomUserSession(),
                new[] {new CustomAuthProvider()}
                ));

            //ApplicationEx.Start(DbCnnFactoryManager.CreateCnnFactory("sqlserver", DbProvider.MsSqlProvider));
        }

        public string IsLogin()
        {
            var authService = AppHostBase.Resolve<AuthService>();
            authService.RequestContext = System.Web.HttpContext.Current.ToRequestContext();
            var session = authService.GetSession(false);


            return session == null ? "" : session.UserName;
        }

        public bool HasRole(string role)
        {
             var authService = AppHostBase.Resolve<AuthService>();
            authService.RequestContext = System.Web.HttpContext.Current.ToRequestContext();
            var session = authService.GetSession(false);
            return session.HasRole(role);

        }

        public void Logout()
        {
            var authService = AppHostBase.Resolve<AuthService>();
            authService.RequestContext = System.Web.HttpContext.Current.ToRequestContext();
            var session = authService.GetSession(false);
            if (session!=null)
            {
                authService.RemoveSession();
            }
        }
    }
}