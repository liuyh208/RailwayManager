// ***********************************************************************
// Assembly         : GasWebMap.Core
// Author           : liuyh
// Created          : 03-21-2013
//
// Last Modified By : liuyh
// Last Modified On : 03-29-2013
// ***********************************************************************
// <copyright file="EntityBase.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;

namespace GasWebMap.Core.Data
{
    /// <summary>
    ///     实体基类,所有的业务对象都必须继承该基类
    /// </summary>
    /// <typeparam name="Tid">主键的类型</typeparam>
    [Serializable]
    public class EntityBase<Tid> : IEntityBase<Tid>
    {
        #region IEntityBase<Tid> Members

        /// <summary>
        ///     唯一主键
        /// </summary>
        /// <value>The id.</value>
        [Key]
        public Tid Id { get; set; }

        #endregion

        #region Override

        /// <summary>
        ///     <see cref="M:System.Object.GetHashCode" />
        /// </summary>
        /// <returns>
        ///     <see cref="M:System.Object.GetHashCode" />
        /// </returns>
        public override int GetHashCode()
        {
            if (Id.Equals(null))
            {
                return base.GetHashCode();
            }
            return Id.GetHashCode() ^ 31;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase<Tid>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var item = (EntityBase<Tid>) obj;
            return item.Id.Equals(Id);
        }

        /// <summary>
        ///     Implements the ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(EntityBase<Tid> left, EntityBase<Tid> right)
        {
            if (Equals(left, null))
                return (Equals(right, null)) ? true : false;
            return left.Equals(right);
        }

        /// <summary>
        ///     Implements the !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(EntityBase<Tid> left, EntityBase<Tid> right)
        {
            return !(left == right);
        }

        #endregion
    }

    /// <summary>
    ///     实体基类,所有的业务对象都必须继承该基类,主键是<c>Guid?</c>
    /// </summary>
    [Serializable]
    public class EntityBase : EntityBase<Guid>, IEntityBase
    {
    }
}