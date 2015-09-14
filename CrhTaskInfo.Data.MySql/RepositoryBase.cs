using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using ServiceStack.OrmLite;

namespace GasWebMap.Repository.MySql
{
    public class Repository<T, Tid> : IRepository<T, Tid> where T : IEntityBase<Tid>, new()
    {
        public Repository()
        {
            //  Console.WriteLine(typeof(T).ToString());
            System.Diagnostics.Debug.WriteLine(typeof(T).ToString());
        }
        private IDbConnection _cnn = null;
        private IDbConnection Cnn
        {
            get
            {
                if (_cnn == null)
                {
                    var factory = AppEx.Container.GetInstance<IDbCnnFactory>();
                    this._cnn = factory.OpenConnection();
                }

                return _cnn;
            }
        }

        private void CloseCnn()
        {
            _cnn.Close();
            _cnn = null;
        }

        #region IRepository<T,Tid> Members

        public void Add(T entity)
        {
            Cnn.Insert(entity);
            CloseCnn();
        }

        public void Add(IEnumerable<T> entities)
        {
            Cnn.InsertAll(entities);
            CloseCnn();
        }

        public void Update(T entity)
        {
            Cnn.Update(entity);
            CloseCnn();
        }

        public void Update(IEnumerable<T> entities)
        {
            Cnn.UpdateAll(entities);
            CloseCnn();
        }

        public void Delete(T entity)
        {
            Cnn.Delete(entity);
            CloseCnn();
        }

        public void Delete(IEnumerable<T> entities)
        {
            Cnn.Delete(entities.ToArray());
            CloseCnn();
        }

        public void DeleteByID(object id)
        {
            Cnn.DeleteById<T>(id);
            CloseCnn();
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            Cnn.Delete(filter);
            CloseCnn();
        }

        public void DeleteByIDs(IEnumerable<Tid> ids)
        {
            Cnn.DeleteByIds<T>(ids);
            CloseCnn();
        }

        public T GetEntityByID(object id)
        {
            var t= Cnn.GetById<T>(id);
            CloseCnn();
            return t;
        }

        public T GetEntity(Expression<Func<T, bool>> filter)
        {
            var t= Cnn.FirstOrDefault(filter);
            CloseCnn();
            return t;
        }

        public IEnumerable<T> GetEntities()
        {
            var t= Cnn.Select<T>();
            CloseCnn();
            return t;
        }

        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter)
        {
            var t= Cnn.Where(filter);
            CloseCnn();
            return t;
        }

        public IEnumerable<T> GetEntities<S>(Expression<Func<T, bool>> filter, Expression<Func<T, S>> orderByExpression,
            bool ascending = true)
        {
            SqlExpressionVisitor<T> visitor = Cnn.GetDialectProvider().ExpressionVisitor<T>();
            visitor.Where(filter);
            if (ascending)
            {
                visitor.OrderBy(orderByExpression);
            }
            else
            {
                visitor.OrderByDescending(orderByExpression);
            }
            var t= Cnn.Select(visitor);
            CloseCnn();
            return t;
        }

        public PageResult<T> GetPagedEntities(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1)
                pageSize = 10;
            var count = Cnn.Count<T>();
            SqlExpressionVisitor<T> visitor = Cnn.GetDialectProvider().ExpressionVisitor<T>();

            visitor.Limit((pageIndex - 1) * pageSize, pageSize);

            var t= new PageResult<T>(Cnn.Select(visitor), (int)count);
            CloseCnn();
            return t;
        }

        public PageResult<T> GetPagedEntities(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1)
                pageSize = 10;
            var count = Cnn.Count<T>();
            SqlExpressionVisitor<T> visitor = Cnn.GetDialectProvider().ExpressionVisitor<T>();
            visitor.Where(filter);
            visitor.Limit((pageIndex - 1) * pageSize, pageSize);

            var t= new PageResult<T>(Cnn.Select(visitor), (int)count);
            CloseCnn();
            return t;
        }

        public PageResult<T> GetPagedEntities<S>(Expression<Func<T, bool>> filter,
            Expression<Func<T, S>> orderByExpression, bool ascending, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1)
                pageSize = 10;
            var count = Cnn.Count<T>();
            SqlExpressionVisitor<T> visitor = Cnn.GetDialectProvider().ExpressionVisitor<T>();
            visitor.Where(filter);
            visitor.Limit((pageIndex - 1) * pageSize, pageSize);
            if (ascending)
            {
                visitor.OrderBy(orderByExpression);
            }
            else
            {
                visitor.OrderByDescending(orderByExpression);
            }

            var t= new PageResult<T>(Cnn.Select(visitor), (int)count);
            CloseCnn();
            return t;
        }

        public void Save()
        {
        }

        #endregion


        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter, string orderBy, bool ascending = true)
        {
            SqlExpressionVisitor<T> visitor = Cnn.GetDialectProvider().ExpressionVisitor<T>();
            visitor.Where(filter);
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                if (ascending)
                {
                    visitor.OrderBy("order by " + orderBy);
                }
                else
                {
                    visitor.OrderBy("order by " + orderBy + " desc");
                }
            }

            var t= Cnn.Select(visitor);
            CloseCnn();
            return t;
        }
    }

    public class Repository<T> : Repository<T, Guid>, IRepository<T> where T : IEntityBase, new()
    {
    }
}