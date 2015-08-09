// ***********************************************************************
// Assembly         : GasWebMap.Domain
// Author           : Liuyh
// Created          : 02-14-2014 11:08:54
//
// Last Modified By : Liuyh
// Last Modified On : 02-14-2014 11:10:10
// ***********************************************************************
// <copyright file="Department.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using GasWebMap.Core.Data;
using ServiceStack.ServiceHost;

/// <summary>
/// The Entities namespace.
/// </summary>

namespace GasWebMap.Domain
{
    /// <summary>
    ///     Class Department.
    /// </summary>
    [Route("/department", "GET")]
    public class Department : EntityBase
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        public Guid? ParentID { get; set; }
    }
}