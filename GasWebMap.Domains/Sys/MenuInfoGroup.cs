// ***********************************************************************
// Assembly         : GasWebMap.Domain
// Author           : Liuyh
// Created          : 02-14-2014 13:31:10
//
// Last Modified By : Liuyh
// Last Modified On : 02-14-2014 13:33:12
// ***********************************************************************
// <copyright file="MenuInfoGroup.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using GasWebMap.Core.Data;
using ServiceStack.ServiceHost;

/// <summary>
/// The Domain namespace.
/// </summary>

namespace GasWebMap.Domain
{
    /// <summary>
    ///     Class MenuInfoGroup.
    /// </summary>
    [Route("/group", "GET")]
    public class MenuInfoGroup : EntityBase
    {
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

        public Guid? ParentID { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        /// <value><c>true</c> 如果 valid; 否则, <c>false</c>.</value>
        public bool Valid { get; set; }
    }
}