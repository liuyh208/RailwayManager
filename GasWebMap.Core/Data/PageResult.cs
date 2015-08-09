// ***********************************************************************
// Assembly         : GasWebMap.Core
// Author           : liuyh
// Created          : 03-21-2013
//
// Last Modified By : liuyh
// Last Modified On : 03-21-2013
// ***********************************************************************
// <copyright file="PageResult.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace GasWebMap.Core.Data
{
    /// <summary>
    ///     Class PageResult
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    public class PageResult<TEntity>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PageResult{TEntity}" /> class.
        /// </summary>
        /// <param name="entites">提取的分页数据</param>
        /// <param name="totalCount">实体的总数</param>
        public PageResult(IEnumerable<TEntity> entites, int totalCount)
        {
            Result = entites;
            Total = totalCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="entites">提取的分页数据</param>
        public PageResult(IEnumerable<TEntity> entites)
            : this(entites, entites.Count())
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     分页数据
        /// </summary>
        /// <value>The result.</value>
        public IEnumerable<TEntity> Result { get; private set; }

        /// <summary>
        ///     结果总数
        /// </summary>
        /// <value>The total count.</value>
        public int Total { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        ///     计算页数
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <returns>System.Int32.</returns>
        public int TotalPages(int pageSize)
        {
            return (int)Math.Ceiling(Convert.ToDouble(Total) / pageSize);
        }

        #endregion Methods
    }
}