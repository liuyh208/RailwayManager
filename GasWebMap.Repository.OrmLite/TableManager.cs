using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Reflection;
using GasWebMap.Core.Data;
using ServiceStack.OrmLite;

namespace GasWebMap.Repository.Oracle
{
    [Export(typeof (ITableManager))]
    public class TableManager : ITableManager
    {
        private IDictionary<Type, Type> dicTypes = new Dictionary<Type, Type>();

        public TableManager(IDbCnnFactory dbFactory)
        {
            DbCnnFactory = dbFactory;
        }

        private IDbCnnFactory DbCnnFactory { get; set; }

        #region ITableManager Members

        /// <summary>
        ///     创建表
        /// </summary>
        /// <param name="overwrite">是否覆盖已有表</param>
        /// <param name="tables">表类型</param>
        public void CreateTable(bool overwrite, params Type[] tables)
        {
            using (IDbConnection cnn = DbCnnFactory.OpenConnection())
            {
                cnn.CreateTables(overwrite, tables);
            }
        }

        /// <summary>
        ///     创建表
        /// </summary>
        /// <typeparam name="T">表类型</typeparam>
        /// <param name="overwrite">是否覆盖已有表</param>
        public void CreateTable<T>(bool overwrite) where T : new()
        {
            using (IDbConnection cnn = DbCnnFactory.OpenConnection())
            {
                cnn.CreateTable<T>(overwrite);
            }
        }

        /// <summary>
        ///     创建表
        /// </summary>
        /// <typeparam name="T">表类型</typeparam>
        /// <param name="overwrite">是否覆盖已有表</param>
        public void CreateTableWithSameNameSpace<T>(bool overwrite) where T : new()
        {
            List<Type> lst = GetTypesWithSameNameSpace(typeof (T));
            using (IDbConnection cnn = DbCnnFactory.OpenConnection())
            {
                cnn.CreateTableIfNotExists(lst.ToArray());
            }
        }

        /// <summary>
        ///     删除表
        /// </summary>
        /// <typeparam name="T">表类型</typeparam>
        public void DropTable<T>() where T : new()
        {
            using (IDbConnection cnn = DbCnnFactory.OpenConnection())
            {
                cnn.DropTable<T>();
            }
        }

        #endregion

        private List<Type> GetTypesWithSameNameSpace(Type type)
        {
            var results = new List<Type>();
            if (type == null)
            {
                return results;
            }
            string ns = type.Namespace;
            Assembly assembly = type.Assembly;
            try
            {
                ;
                foreach (Type t in assembly.GetTypes())
                {
                    if (t.Namespace == ns)
                    {
                        results.Add(t);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     填充某模型的 DisplayName 属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="displayNames"></param>
        public void FillDisplayNameAttribute(Type type, IDictionary<string, string> displayNames)
        {
            if (type == null || displayNames == null || displayNames.Count == 0)
            {
                return;
            }

            PropertyInfo targetProperty = typeof (MemberDescriptor).GetProperty("AttributeArray",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);
            string displayName;
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(type))
            {
                if (displayNames.TryGetValue(property.Name, out displayName) == false)
                {
                    continue;
                }
                var attributeList = new List<Attribute>(targetProperty.GetValue(property, null) as Attribute[]);
                attributeList.RemoveAll(delegate(Attribute attrib) { return attrib is DisplayNameAttribute; });
                attributeList.Add(new DisplayNameAttribute(displayName));
                targetProperty.SetValue(property, attributeList.ToArray(), null);
            }
        }
    }
}