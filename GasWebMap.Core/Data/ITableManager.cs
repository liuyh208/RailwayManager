using System;

namespace GasWebMap.Core.Data
{
    public interface ITableManager
    {
        /// <summary>
        ///     根据对象实体创建数据表,相同命名空间的实体对象都会创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="overwrite">如果数据表存在,当设置为<c>true</c> 时,则重建表</param>
        void CreateTableWithSameNameSpace<T>(bool overwrite) where T : new();

        /// <summary>
        ///     根据对象实体创建数据表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="overwrite">如果数据表存在,当设置为<c>true</c> 时,则重建表</param>
        void CreateTable<T>(bool overwrite) where T : new();

        /// <summary>
        ///     根据对象实体创建数据表
        /// </summary>
        /// <param name="overwrite">如果数据表存在,当设置为<c>true</c> 时,则重建表</param>
        /// <param name="tables">实体类型</param>
        void CreateTable(bool overwrite, params Type[] tables);

        /// <summary>
        ///     删除表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        void DropTable<T>() where T : new();
    }
}