using System.Collections.Generic;
using GasWebMap.Core.Data;

namespace System.Linq
{
    public static class IQueryableExtension
    {
        public static PageResult<T> ToPageResult<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            int count = query.Count();
            IQueryable<T> lst = query.Skip((pageSize - 1)*pageSize).Take(pageSize);
            return new PageResult<T>(lst, count);
        }

        /// <summary>
        ///     分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>分页后的结果</returns>
        public static PageResult<T> ToPageResult<T>(this IEnumerable<T> query, int pageIndex, int pageSize)
        {
            int count = query.Count();
            IEnumerable<T> lst = query.Skip(((pageIndex - 1)*pageSize)).Take(pageSize);
            return new PageResult<T>(lst, count);
        }
    }
}