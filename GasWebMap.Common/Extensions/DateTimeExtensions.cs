// ***********************************************************************
// Assembly         : GasWebMap.Core
// Author           : liuyh
// Created          : 05-29-2013
//
// Last Modified By : liuyh
// Last Modified On : 05-29-2013
// ***********************************************************************
// <copyright file="DateTimeExtensions.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace System
{
    /// <summary>
    ///     Class DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     转换成字符串 (yyyyMMdd HH:mm:ss)
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>System.String.</returns>
        public static string ToCustomString(this DateTime dt)
        {
            return dt.ToString("yyyyMMdd HH:mm:ss");
        }

        /// <summary>
        ///     转换成字符串 (yyyyMMddHHmmss)
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>System.String.</returns>
        public static string ToCustomString1(this DateTime dt)
        {
            return dt.ToString("yyyyMMddHHmmss");
        }
    }
}