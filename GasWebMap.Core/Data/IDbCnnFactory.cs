// ***********************************************************************
// Assembly         : GasWebMap.Core
// Author           : liuyh
// Created          : 03-26-2013
//
// Last Modified By : liuyh
// Last Modified On : 03-29-2013
// ***********************************************************************
// <copyright file="IDbCnnFactory.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Data;

namespace GasWebMap.Core.Data
{
    /// <summary>
    ///     Interface IDbCnnFactory
    /// </summary>
    public interface IDbCnnFactory
    {
        /// <summary>
        ///     打开一个数据库连接
        /// </summary>
        /// <returns>IDbConnection.</returns>
        IDbConnection OpenConnection();

        /// <summary>
        ///     创建一个数据库连接
        /// </summary>
        /// <returns>IDbConnection.</returns>
        IDbConnection CreateConnection();
    }
}