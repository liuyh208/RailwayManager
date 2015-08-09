// ***********************************************************************
// Assembly         : GasWebMap.Services
// Author           : Liuyh
// Created          : 02-12-2014 13:52:37
//
// Last Modified By : Liuyh
// Last Modified On : 02-14-2014 14:01:40
// ***********************************************************************
// <copyright file="ServiceBase.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using ServiceStack.ServiceInterface;

/// <summary>
/// The Base namespace.
/// </summary>

namespace GasWebMap.Services.Base
{
    /// <summary>
    ///     Class ServiceBase.
    /// </summary>
    public abstract class ServiceBase : Service
    {
        /// <summary>
        ///     读取缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存键值</param>
        /// <returns>``0.</returns>
        protected T GetCache<T>(string key, Func<T> funGet = null)
        {
            var t = Cache.Get<T>(key);
            if (t == null && funGet != null)
            {
                t = funGet();
                Cache.Add(key, t);
            }
            return t;
        }

        /// <summary>
        ///     读取缓存数据（返回Ilist）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存键值</param>
        /// <param name="funGet">当缓存中值为null时，执行该方法获得数据，并写入缓存</param>
        /// <returns>IList{``0}.</returns>
        protected IList<T> GetCacheList<T>(string key, Func<IList<T>> funGet = null)
        {
            var lst = Cache.Get<IList<T>>(key);
            if (lst == null && funGet != null)
            {
                lst = funGet();
                Cache.Add(key, lst);
            }
            return lst;
        }

        protected void AddToCache<T>(string key, T value)
        {
            Cache.Set(key, value);
        }

        protected void AddToCache<T>(string key, IList<T> value)
        {
            Cache.Set(key, value);
        }

        protected void RemoveCache(string key)
        {
            Cache.Remove(key);
        }

        protected IRepository<T> GetRepository<T>() where T : EntityBase, new()
        {
            return AppEx.Container.GetRepository<T>();
        }
    }
}