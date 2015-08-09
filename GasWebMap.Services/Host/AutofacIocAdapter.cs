using GasWebMap.Core;
using ServiceStack.Configuration;

namespace GasWebMap.Services.Host
{
    public class AutofacIocAdapter : IContainerAdapter
    {
        private readonly IocContainer _container;

        public AutofacIocAdapter(IocContainer container)
        {
            _container = container;
        }

        #region IContainerAdapter Members

        public T Resolve<T>()
        {
            return _container.GetInstance<T>();
        }

        public T TryResolve<T>()
        {
            return Resolve<T>();
        }

        #endregion
    }
}