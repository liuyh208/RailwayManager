using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace GasWebMap.Core.Data
{
    public interface IDbContext : IDisposable
    {
        //DbSet<T> Set<T>() where T : class;

        void Delete<T>(params T[] entity) where T : class;

        void Delete<T>(Expression<Func<T, bool>> filter) where T : class;

        void AddOrUpdate<T>(params T[] entities) where T : class;


        IQueryable<T> Table<T>() where T : class;
       
        List<T> ExcuteSql<T>(string sql);

        int ExcuteSql(string sql);
        int SaveChanges();


    }

    public interface ISafeDelete
    {
        bool IsDelete { get; set; }
    }

}
