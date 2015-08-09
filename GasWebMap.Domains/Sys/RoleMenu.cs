// ***********************************************************************
// Assembly         : GasWebMap.Domain
// Author           : Liuyh
// Created          : 02-14-2014 13:46:44
//
// Last Modified By : Liuyh
// Last Modified On : 02-14-2014 13:47:28
// ***********************************************************************
// <copyright file="RoleMenu.cs" company="Tecocity">
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
    ///     Class RoleMenu.
    /// </summary>
    public class RoleMenu : EntityBase
    {
        /// <summary>
        ///     角色ID
        /// </summary>
        /// <value>The role identifier.</value>
        public Guid RoleID { get; set; }

        /// <summary>
        ///     菜单ID
        /// </summary>
        /// <value>The menu identifier.</value>
        public Guid MenuID { get; set; }
    }
}