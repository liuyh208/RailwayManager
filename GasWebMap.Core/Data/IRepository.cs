// ***********************************************************************
// Assembly         : GasWebMap.Core
// Author           : liuyh
// Created          : 03-26-2013
//
// Last Modified By : liuyh
// Last Modified On : 03-21-2013
// ***********************************************************************
// <copyright file="IRepository.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GasWebMap.Core.Data
{
    /// <summary>
    ///     Interface IRepository
    /// </summary>
    /// <typeparam name="T">业务实体</typeparam>
    /// <typeparam name="Tid">业务实体的主键类型</typeparam>
    public interface IRepository<T, Tid> where T : IEntityBase<Tid>, new()
    {
        /// <summary>
        ///     添加数据
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        ///     批量添加数据
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Add(IEnumerable<T> entities);

        /// <summary>
        ///     更新数据
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        ///     批量更新数据
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        ///     Delete item
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        ///     批量删除数据
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        ///     根据主键删除数据
        /// </summary>
        /// <param name="id">The id.</param>
        void DeleteByID(object id);

        /// <summary>
        ///     删除符合条件的实体
        /// </summary>
        /// <param name="filter"></param>
        void Delete(Expression<Func<T, bool>> filter);

        /// <summary>
        ///     根据主键批量删除数据
        /// </summary>
        /// <param name="ids">The ids.</param>
        void DeleteByIDs(IEnumerable<Tid> ids);

        /// <summary>
        ///     通过ID读取数据
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>`0.</returns>
        T GetEntityByID(object id);

        /// <summary>
        ///     获得符合条件的第一个实体
        /// </summary>
        /// <param name="filter"></param>
        T GetEntity(Expression<Func<T, bool>> filter);

        /// <summary>
        ///     读取全部数据
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<T> GetEntities();

        /// <summary>
        ///     根据过滤条件读取数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter);

        IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter,
            string orderBy, bool ascending = true);
        /// <summary>
        ///     根据过滤条件和排序条件读取数据
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="filter">过滤条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="ascending">是否为正序</param>
        /// <returns>IEnumerable{`0}.</returns>
        IEnumerable<T> GetEntities<S>(Expression<Func<T, bool>> filter,
            Expression<Func<T, S>> orderByExpression, bool ascending = true);

        /// <summary>
        ///     分页读取数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>List of selected elements</returns>
        PageResult<T> GetPagedEntities(int pageIndex, int pageSize);

        /// <summary>
        ///     分页读取数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>List of selected elements</returns>
        PageResult<T> GetPagedEntities(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);

        /// <summary>
        ///     分页读取数据
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="filter">过滤条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="ascending">是否正序</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>List of selected elements</returns>
        PageResult<T> GetPagedEntities<S>(Expression<Func<T, bool>> filter, Expression<Func<T, S>> orderByExpression,
            bool ascending, int pageIndex, int pageSize);

        void Save();
    }

    /// <summary>
    ///     Interface IRepository
    /// </summary>
    /// <typeparam name="TEntity">业务实体</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : IEntityBase<Guid>, new()
    {
    }
}