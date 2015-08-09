using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using ServiceStack.OrmLite;

namespace GasWebMap.Repository.Oracle
{
    public class Repository<T, Tid> : IRepository<T, Tid> where T : IEntityBase<Tid>, new()
    {
        private IDbConnection Cnn
        {
            get
            {
                var factory = AppEx.Container.GetInstance<IDbCnnFactory>();
                return factory.OpenConnection();
            }
        }

        #region IRepository<T,Tid> Members

        public void Add(T entity)
        {
            Cnn.Insert(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            Cnn.InsertAll(entities);
        }

        public void Update(T entity)
        {
            Cnn.Update(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            Cnn.UpdateAll(entities);
        }

        public void Delete(T entity)
        {
            Cnn.Delete(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            Cnn.Delete(entities.ToArray());
        }

        public void DeleteByID(object id)
        {
            Cnn.DeleteById<T>(id);
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            Cnn.Delete(filter);
        }

        public void DeleteByIDs(IEnumerable<Tid> ids)
        {
            Cnn.DeleteByIds<T>(ids);
        }

        public T GetEntityByID(object id)
        {
            return Cnn.GetById<T>(id);
        }

        public T GetEntity(Expression<Func<T, bool>> filter)
        {
            return Cnn.FirstOrDefault(filter);
        }

        public IEnumerable<T> GetEntities()
        {
            return Cnn.Each<T>();
        }

        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter)
        {
            return Cnn.Where(filter);
        }

        public IEnumerable<T> GetEntities<S>(Expression<Func<T, bool>> filter, Expression<Func<T, S>> orderByExpression,
            bool ascending = true)
        {
            if (ascending)
            {
                return Cnn.Where(filter).OrderBy(orderByExpression.Compile());
            }
            return Cnn.Where(filter).OrderByDescending(orderByExpression.Compile());
        }

        public PageResult<T> GetPagedEntities(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1)
                pageSize = 10;

            return Cnn.Each<T>().ToPageResult<T>(pageIndex, pageSize);
        }

        public PageResult<T> GetPagedEntities(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1)
                pageSize = 10;

            return Cnn.Where(filter).ToPageResult(pageIndex, pageSize);
        }

        public PageResult<T> GetPagedEntities<S>(Expression<Func<T, bool>> filter,
            Expression<Func<T, S>> orderByExpression, bool ascending, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1)
                pageSize = 10;

            if (ascending)
            {
                return Cnn.Where(filter).OrderBy(orderByExpression.Compile()).ToPageResult(pageIndex, pageSize);
            }
            return Cnn.Where(filter).OrderByDescending(orderByExpression.Compile()).ToPageResult(pageIndex, pageSize);
        }

        public void Save()
        {
        }

        #endregion
    }

    public class Repository<T> : Repository<T, Guid>, IRepository<T> where T : IEntityBase, new()
    {
    }
}