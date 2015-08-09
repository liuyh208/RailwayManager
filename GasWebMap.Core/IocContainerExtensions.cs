using GasWebMap.Core.Data;

namespace GasWebMap.Core
{
    public static class IocContainerExtensions
    {
        /// <summary>
        ///     获得数据操作仓库接口
        /// </summary>
        /// <typeparam name="T">操作的实体类</typeparam>
        /// <returns>IRepository{``0}.</returns>
        public static IRepository<T> GetRepository<T>(this IocContainer ioc) where T : EntityBase, new()
        {
            return ioc.GetInstance<IRepository<T>>();
        }

        /// <summary>
        ///     获得数据操作仓库接口
        /// </summary>
        /// <typeparam name="T">操作的实体类</typeparam>
        /// <typeparam name="Tid">实体的主键类型</typeparam>
        /// <returns>IRepository{``0``1}.</returns>
        public static IRepository<T, Tid> GetRepository<T, Tid>(this IocContainer ioc) where T : EntityBase<Tid>, new()
        {
            return ioc.GetInstance<IRepository<T, Tid>>();
        }

        #region Nested type: testData

        internal class testData : EntityBase
        {
        }

        #endregion
    }
}