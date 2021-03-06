﻿// ***********************************************************************
// Assembly         : GasMap.Core
// Author           : liuyh
// Created          : 04-02-2013
//
// Last Modified By : liuyh
// Last Modified On : 04-02-2013
// ***********************************************************************
// <copyright file="IContainer.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace GasWebMap.Core
{
    /// <summary>
    ///     Interface IocContainer
    /// </summary>
    public interface IocContainer : IDisposable
    {
        /// <summary>
        ///     获得某个接口的一组实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable{``0}.</returns>
        IEnumerable<T> GetInstances<T>();

        /// <summary>
        ///     获取接口的一个实例
        /// </summary>
        /// <typeparam name="T">接口的类型</typeparam>
        /// <returns>，如果没有注册该接口，则返回null</returns>
        T GetInstance<T>();

        /// <summary>
        ///     根据关键字获取对象
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        object GetInstance(string key);
    }
}