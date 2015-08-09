using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace GasWebMap.Core
{
    public class MefContainer : IocContainer
    {
        private readonly CompositionContainer _container;

        public MefContainer(AggregateCatalog aggCatalog)
        {
            //AggregateCatalog agg=new AggregateCatalog(catalogs);
            _container = new CompositionContainer(aggCatalog);
        }

        public Func<string, object> CustomInstanceCreate { get; set; }

        #region IocContainer Members

        public IEnumerable<T> GetInstances<T>()
        {
            return _container.GetExportedValues<T>();
        }

        public T GetInstance<T>()
        {
            return _container.GetExportedValueOrDefault<T>();
        }

        public object GetInstance(string key)
        {
            return CustomInstanceCreate(key);
        }

        public void Dispose()
        {
            if (_container != null)
                _container.Dispose();
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

        private bool RegisterOrm(string filepath)
        {
            bool isload = false;

            //Assembly ass = Assembly.LoadFile(filepath);
            //Type[] types = ass.GetTypes().Where(t => t.Name.EndsWith("Repository`1")).ToArray();
            //if (types.Length > 0 && types[0].Name != "IRepository`1")
            //{
            //    builder.RegisterGeneric(types[0]).As(typeof(IRepository<>));

            //    isload = true;
            //}
            //types = ass.GetTypes().Where(t => t.Name.EndsWith("Repository`2")).ToArray();
            //if (types.Length > 0 && types[0].Name != "IRepository`2")
            //{
            //    builder.RegisterGeneric(types[0]).As(typeof(IRepository<,>));
            //    isload = true;
            //}
            return isload;
        }
    }
}