using GasWebMap.Core.Data;

namespace GasWebMap.Core
{
    public static class IocContainerExtensions
    {
        /// <summary>
        ///     ������ݲ����ֿ�ӿ�
        /// </summary>
        /// <typeparam name="T">������ʵ����</typeparam>
        /// <returns>IRepository{``0}.</returns>
        public static IRepository<T> GetRepository<T>(this IocContainer ioc) where T : EntityBase, new()
        {
            return ioc.GetInstance<IRepository<T>>();
        }

        /// <summary>
        ///     ������ݲ����ֿ�ӿ�
        /// </summary>
        /// <typeparam name="T">������ʵ����</typeparam>
        /// <typeparam name="Tid">ʵ�����������</typeparam>
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