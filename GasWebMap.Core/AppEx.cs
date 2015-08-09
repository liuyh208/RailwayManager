using System;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace GasWebMap.Core
{
    public class AppEx
    {
        protected static ILogger Log = LogFactory.GetLogger(typeof (AppEx));
        private static bool pIsFirst = true;

        private static bool pRequireLogin = true;

        private AppEx()
        {
        }

        public static IocContainer Container { get; private set; }

        public static void Start()
        {
            Log.Info("Start Program!");
            Init();
            Log.Info("Init Ok!");
        }

        private static void Init()
        {
            try
            {
                string dlls = ConfigurationManager.AppSettings["ImportDlls"];

                var aggregatecatalogue = new AggregateCatalog();
                aggregatecatalogue.Catalogs.Add(new AssemblyCatalog(Assembly.GetCallingAssembly()));
                if (!string.IsNullOrWhiteSpace(dlls))
                {
                    string[] dds = dlls.Split(';');
                    foreach (string item in dds)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            aggregatecatalogue.Catalogs.Add(
                                new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory + @"bin", item));
                        }
                    }
                }

                Container = new AutofacContainer(aggregatecatalogue.Catalogs.ToArray());
                var aContainer = Container as AutofacContainer;
            }
            catch (Exception ex)
            {
                Log.Error("初始化错误", ex);
            }
        }

        /// <summary>
        ///     退出程序
        /// </summary>
        public static void Stop()
        {
            Container.Dispose();
        }
    }
}