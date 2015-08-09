// ***********************************************************************
// Assembly         : GasWebMap.Domain
// Author           : Liuyh
// Created          : 02-14-2014 13:44:54
//
// Last Modified By : Liuyh
// Last Modified On : 02-14-2014 13:45:47
// ***********************************************************************
// <copyright file="UserRole.cs" company="Tecocity">
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
    ///     Class UserRole.
    /// </summary>
    public class UserRole : EntityBase
    {
        /// <summary>
        ///     用户ID
        /// </summary>
        /// <value>The user identifier.</value>
        public Guid UserID { get; set; }

        /// <summary>
        ///     角色ID
        /// </summary>
        /// <value>The role identifier.</value>
        public Guid RoleID { get; set; }
    }
}