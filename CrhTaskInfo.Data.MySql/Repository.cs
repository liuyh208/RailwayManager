//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Linq.Expressions;
//using GasWebMap.Core;
//using GasWebMap.Core.Data;
//using ServiceStack.Common.Extensions;


//namespace GasWebMap.Repository.MySql
//{
//    public class Repository<T, Tid> : IRepository<T, Tid> where T :class, IEntityBase<Tid> , new()
//    {
//        private readonly IDbContext _context;

//        public Repository(IDbContext context)
//        {
//            _context = context;
//            //  Console.WriteLine(typeof(T).ToString());
//            System.Diagnostics.Debug.WriteLine(typeof(T).ToString());
//        }

//        #region IRepository<T,Tid> Members

//        public void Add(T entity)
//        {
//           // Cnn.Insert(entity);
//            _context.AddOrUpdate<T>(entity);
//        }

//        public void Add(IEnumerable<T> entities)
//        {
//            _context.AddOrUpdate<T>(entities.ToArray());
//        }

//        public void Update(T entity)
//        {
//            _context.AddOrUpdate<T>(entity);
//        }

//        public void Update(IEnumerable<T> entities)
//        {
//            _context.AddOrUpdate<T>(entities.ToArray());
//            _context.SaveChanges();
//        }

//        public void Delete(T entity)
//        {
//           _context.Delete(entity);
//           _context.SaveChanges();
//        }

//        public void Delete(IEnumerable<T> entities)
//        {
//            _context.Delete(entities);
//            _context.SaveChanges();
//        }

//        public void DeleteByID(object id)
//        {
//           var item=  _context.Table<T>().FirstOrDefault(t => t.Id.Equals(id));
//            if (item!=null)
//            {
//                _context.Delete(item);
//                _context.SaveChanges();
//            }

//        }

//        public void Delete(Expression<Func<T, bool>> filter)
//        {
//            _context.Delete(filter);
//            _context.SaveChanges();
//        }

//        public void DeleteByIDs(IEnumerable<Tid> ids)
//        {
//            foreach (var id in ids)
//            {
//                DeleteByID(id);
//            }
//        }

//        public T GetEntityByID(object id)
//        {
//            return _context.Table<T>().FirstOrDefault(t => t.Id.Equals(id));
//        }

//        public T GetEntity(Expression<Func<T, bool>> filter)
//        {
//            return _context.Table<T>().FirstOrDefault(filter);
//        }

//        public IEnumerable<T> GetEntities()
//        {
//            return _context.Table<T>();
//        }

//        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter)
//        {
//            return _context.Table<T>().Where(filter);
//        }

//        public IEnumerable<T> GetEntities<S>(Expression<Func<T, bool>> filter, Expression<Func<T, S>> orderByExpression,
//            bool ascending = true)
//        {
//            if (ascending)
//            {
//                return _context.Table<T>().Where(filter).OrderBy(orderByExpression);
//            }
//            else
//            {
//                return _context.Table<T>().Where(filter).OrderByDescending(orderByExpression);
//            }

//        }

//        public PageResult<T> GetPagedEntities(int pageIndex, int pageSize)
//        {
//            if (pageIndex < 1)
//                pageIndex = 1;
//            if (pageSize < 1)
//                pageSize = 10;
//            return  _context.Table<T>().ToPageResult(pageIndex, pageSize);

            
//        }

//        public PageResult<T> GetPagedEntities(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
//        {
//            if (pageIndex < 1)
//                pageIndex = 1;
//            if (pageSize < 1)
//                pageSize = 10;
//            return _context.Table<T>().Where(filter).ToPageResult(pageIndex, pageSize);

//        }

//        public PageResult<T> GetPagedEntities<S>(Expression<Func<T, bool>> filter,
//            Expression<Func<T, S>> orderByExpression, bool ascending, int pageIndex, int pageSize)
//        {
//            if (pageIndex < 1)
//                pageIndex = 1;
//            if (pageSize < 1)
//                pageSize = 10;
//            if (ascending)
//            {
//                return _context.Table<T>().Where(filter).OrderBy(orderByExpression).ToPageResult(pageIndex, pageSize);
//            }
//            else
//            {
//                return _context.Table<T>().Where(filter).OrderByDescending(orderByExpression).ToPageResult(pageIndex, pageSize);
//            }

            
            
//        }

//        public void Save()
//        {
//        }

//        #endregion


//        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter, string orderBy, bool ascending = true)
//        {
//            var lst= _context.Table<T>().Where(filter);
//            return lst;
//        }
//    }

//    public class Repository<T> : Repository<T, Guid>, IRepository<T> where T :class, IEntityBase, new()
//    {
//    }
//}