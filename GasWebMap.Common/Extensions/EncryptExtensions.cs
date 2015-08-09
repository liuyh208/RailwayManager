// ***********************************************************************
// Assembly         : GasWebMap.Core
// Author           : liuyh
// Created          : 03-29-2013
//
// Last Modified By : liuyh
// Last Modified On : 03-29-2013
// ***********************************************************************
// <copyright file="HashEncrypt.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    /**/

    /// <summary>
    ///     类名：EncryptExtensions
    ///     作用：对传入的字符串进行Hash运算，返回通过Hash算法加密过的字串。
    ///     属性：［无］
    ///     构造函数额参数：
    ///     IsReturnNum:是否返回为加密后字符的Byte代码
    ///     IsCaseSensitive：是否区分大小写。
    ///     方法：此类提供MD5，SHA1，SHA256，SHA512等四种算法，加密字串的长度依次增大。
    /// </summary>
    public static class EncryptExtensions
    {
        /// <summary>
        ///     格式化字符串
        /// </summary>
        /// <param name="strIN">The STR IN.</param>
        /// <returns>System.String.</returns>
        private static string getstrIN(string strIN)
        {
            //string strIN = strIN;
            if (strIN.Length == 0)
            {
                strIN = "~NU*LL~";
            }

            return strIN;
        }

        /// <summary>
        ///     使用Md5 加密
        /// </summary>
        /// <param name="strIN">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Encrypt(this string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(GetKeyByteArray(getstrIN(strIN)));
            md5.Clear();

            return GetStringValue(tmpByte);
        }

        /// <summary>
        ///     使用Md5 加密
        /// </summary>
        /// <param name="strIN">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Encrypt(this string strIN, Encoding encoding)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(GetKeyByteArray(getstrIN(strIN)));
            md5.Clear();

            return GetStringValue(tmpByte, encoding);
        }

        /// <summary>
        ///     使用SHsA1 加密
        /// </summary>
        /// <param name="strIN">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA1Encrypt(this string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            tmpByte = sha1.ComputeHash(GetKeyByteArray(strIN));
            sha1.Clear();

            return GetStringValue(tmpByte);
        }

        /// <summary>
        ///     使用SHA256 加密
        /// </summary>
        /// <param name="strIN">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA256Encrypt(this string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();

            tmpByte = sha256.ComputeHash(GetKeyByteArray(strIN));
            sha256.Clear();

            return GetStringValue(tmpByte);
        }

        /// <summary>
        ///     使用SHA512 加密
        /// </summary>
        /// <param name="strIN">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA512Encrypt(this string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();

            tmpByte = sha512.ComputeHash(GetKeyByteArray(strIN));
            sha512.Clear();

            return GetStringValue(tmpByte);
        }

        /// <summary>
        ///     使用DES加密
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(this string originalValue, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateEncryptor();

            byt = Encoding.UTF8.GetBytes(originalValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        ///     使用DES加密
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(this string originalValue, string key)
        {
            return DESEncrypt(originalValue, key, key);
        }

        /// <summary>
        ///     使用DES解密
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">m初始化向量(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(this string encryptedValue, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateDecryptor();

            byt = Convert.FromBase64String(encryptedValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        ///     使用DES解密
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(this string encryptedValue, string key)
        {
            return encryptedValue.DESDecrypt(key, key);
        }

        /// <summary>
        ///     将二进制转换为字符串
        /// </summary>
        /// <param name="Byte">待转换的二进制数组</param>
        /// <returns>字符串</returns>
        private static string GetStringValue(byte[] Bytes)
        {
            return GetStringValue(Bytes, Encoding.ASCII);
        }

        /// <summary>
        ///     将二进制转换为字符串
        /// </summary>
        /// <param name="Byte">待转换的二进制数组</param>
        /// <returns>字符串</returns>
        private static string GetStringValue(byte[] Bytes, Encoding encoding)
        {
            return encoding.GetString(Bytes);
        }

        /// <summary>
        ///     将字符串转化换为二进制数组
        /// </summary>
        /// <param name="strKey">待转换的字符串</param>
        /// <returns>二进制数组</returns>
        private static byte[] GetKeyByteArray(string strKey)
        {
            return GetKeyByteArray(strKey, Encoding.ASCII);
        }

        /// <summary>
        ///     将字符串转化换为二进制数组
        /// </summary>
        /// <param name="strKey">待转换的字符串</param>
        /// <returns>二进制数组</returns>
        private static byte[] GetKeyByteArray(string strKey, Encoding encodeing)
        {
            return encodeing.GetBytes(strKey);
            ;
            //int tmpStrLen = strKey.Length;
            //byte[] tmpByte = new byte[tmpStrLen - 1];

            //tmpByte = encodeing.GetBytes(strKey);

            //return tmpByte;
        }
    }
}