// ***********************************************************************
// Assembly         : GasWebMap.Domain
// Author           : Liuyh
// Created          : 02-14-2014 13:06:48
//
// Last Modified By : Liuyh
// Last Modified On : 02-14-2014 13:10:11
// ***********************************************************************
// <copyright file="MenuInfo.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using GasWebMap.Core.Data;

/// <summary>
/// The Domain namespace.
/// </summary>

namespace GasWebMap.Domain
{
    /// <summary>
    ///     Class MenuInfo.
    /// </summary>
    public class MenuInfo : EntityBase
    {
        public MenuInfo()
        {
            Source = "local";
        }

        /// <summary>
        ///     序号
        /// </summary>
        /// <value>The index.</value>
        public int PIndex { get; set; }

        /// <summary>
        ///     显示文本
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        /// <value>The image.</value>
        public string Image { get; set; }

        /// <summary>
        ///     链接的URL
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        /// <value><c>true</c> 如果 valid; 否则, <c>false</c>.</value>
        public bool IsLock { get; set; }

        /// <summary>
        ///     分组ID
        /// </summary>
        /// <value>The group identifier.</value>
        public Guid GroupID { get; set; }

        /// <summary>
        ///     是否只有管理员可见
        /// </summary>
        /// <value><c>true</c> 如果 valid; 否则, <c>false</c>.</value>
        public bool Admin { get; set; }

        /// <summary>
        ///     来源
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }
    }
}