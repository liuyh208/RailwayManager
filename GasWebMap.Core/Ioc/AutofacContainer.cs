using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mef;
using Autofac.Integration.Mvc;
using GasWebMap.Core.Data;

namespace GasWebMap.Core
{
    public class AutofacContainer : IocContainer
    {
        protected static ILogger Log = LogFactory.GetLogger(typeof (AutofacContainer));
        private IContainer _container;
        public ContainerBuilder builder;

        public AutofacContainer(params ComposablePartCatalog[] catalogs)
        {
            builder = new ContainerBuilder();

            //bool isRegisterOrm = false;
            foreach (ComposablePartCatalog item in catalogs)
            {
                builder.RegisterComposablePartCatalog(item);

                //if (item is DirectoryCatalog && !isRegisterOrm)
                //{
                //    DirectoryCatalog dir = item as DirectoryCatalog;
                //    isRegisterOrm = RegistorOrms(dir.LoadedFiles);
                //}
            }

            // builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
        }

        private IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    //_container = builder.Build(Autofac.Builder.ContainerBuildOptions.IgnoreStartableComponents);
                    _container = builder.Build();
                }
                return _container;
            }
        }

        #region IocContainer Members

        public T GetInstance<T>()
        {
            return TryGetInstance<T>();
        }

        public IEnumerable<T> GetInstances<T>()
        {
            IEnumerable<T> e = new List<T>();
            try
            {
                e = Container.Resolve<IEnumerable<T>>();
            }
            catch (Exception ex)
            {
                Log.Error("Ioc 获得实例数组错误" + typeof (T).FullName, ex);
            }
            return e;
        }

        public void Dispose()
        {
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
            }
            builder = null;

            GC.SuppressFinalize(this);
        }

        public object GetInstance(string key)
        {
            throw new NotImplementedException();
        }

        #endregion

        private bool RegistorOrms(IEnumerable<string> filepaths)
        {
            bool isReg = false;
            foreach (string item in filepaths)
            {
                isReg = RegisterOrm(item);
                if (isReg)
                {
                    return true;
                }
            }
            return false;
        }

        public void RegisterGeneric(Type interfaceType, Type objType)
        {
            builder.RegisterGeneric(objType).As(interfaceType);
        }

        public void Register<Tinterface, TClass>()
        {
            //builder.Register<TClass>().As<Tinterface>();

            builder.RegisterType<TClass>().As<Tinterface>();
        }

        private bool RegisterOrm(string filepath)
        {
            bool isload = false;

            Assembly ass = Assembly.LoadFile(filepath);

            Type[] types = ass.GetTypes().Where(t => t.Name.EndsWith("Repository`1")).ToArray();
            if (types.Length > 0 && types[0].Name != "IRepository`1")
            {
                builder.RegisterGeneric(types[0]).As(typeof(IRepository<>));
                isload = true;
            }
            types = ass.GetTypes().Where(t => t.Name.EndsWith("Repository`2")).ToArray();
            if (types.Length > 0 && types[0].Name != "IRepository`2")
            {
                builder.RegisterGeneric(types[0]).As(typeof(IRepository<,>));

                isload = true;
            }
            return isload;
        }

        public T TryGetInstance<T>()
        {
            T t = default(T);
            try
            {
                bool bl = Container.TryResolve(out t);
            }
            catch (Exception ex)
            {
                Log.Error("Ioc 获得实例错误! " + typeof (T).FullName, ex);
            }

            return t;
        }
    }
}