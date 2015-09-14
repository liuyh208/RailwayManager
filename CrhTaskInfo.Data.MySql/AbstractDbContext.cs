//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity.Migrations;
//using System.Data.Entity.ModelConfiguration;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using GasWebMap.Core.Data;


//namespace GasWebMap.Repository.MySql
//{
//   public abstract  class AbstractDbContext : DbContext,IDbContext
//    {

//       public AbstractDbContext(string cnnName)
//            : base(cnnName)
//        {
//        }

//        protected sealed override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
            
//            OnModelCreate(modelBuilder);
//            base.OnModelCreating(modelBuilder);
//            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

           

//        }

//       protected virtual void OnModelCreate(DbModelBuilder modelBuilder)
//       {
//       }


//       public void AddOrUpdate<T>(params T[] entities) where T : class
//        {
//            var db =base.Set<T>();
//            db.AddOrUpdate(entities);
//        }

       

//        public IQueryable<T> Set<T>() where T : class
//        {
//            return Set<T>();
//        }

  
//        public List<T> ExcuteSql<T>(string sql)
//        {
//           return this.Database.SqlQuery<T>(sql).ToList();
//        }

//        public int ExcuteSql(string sql)
//        {
//            if (this.Database.Connection.State != ConnectionState.Open)
//                this.Database.Connection.Open();
//            return this.Database.ExecuteSqlCommand(sql);
//        }



//        public void Delete<T>(params T[] entity) where T : class
//        {
//            if (entity.Length==0)
//            {
//                return;
//            }
//            var db = base.Set<T>();
//            if (entity[0] is ISafeDelete)
//            {

//                foreach (var en in entity)
//                {
//                    var sd = en as ISafeDelete;
//                    sd.IsDelete = true;
//                }
//                db.AddOrUpdate(entity);

//            }
//            else
//            {
//                db.RemoveRange(entity);
//            }
            
//        }

//        public void Delete<T>(Expression<Func<T, bool>> filter) where T : class
//        {
//            var db = base.Set<T>();
//            var lst = db.Where(filter);

//            foreach (var item in lst)
//            {
//                if (item is ISafeDelete)
//                {
//                    var sd = item as ISafeDelete;
//                    sd.IsDelete = true;
//                    db.AddOrUpdate(item);
//                }
//                else
//                {
//                    db.Remove(item);
//                }
//            }


            
//        }


//        public IQueryable<T> Table<T>() where T : class
//        {
//            return Set<T>();
//        }

      
//    }
//}
