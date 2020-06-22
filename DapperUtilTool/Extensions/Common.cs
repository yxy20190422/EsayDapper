using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DapperUtilTool.Extensions
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 获取真实客户端IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRealClientIP(HttpContext context)
        {
            string clientIP = context.Request.Headers["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(clientIP) || (clientIP.ToLower() == "unknown"))
            {
                clientIP = context.Request.Headers["HTTP_X_REAL_IP"];
                if (string.IsNullOrEmpty(clientIP))
                {
                    clientIP = context.Request.Headers["REMOTE_ADDR"];
                }
            }
            else
            {
                clientIP = clientIP.Split(',')[0];
            }
            return clientIP;
        }

        /// <summary>
        /// 字段空返回string.Empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AdjustNullValue(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }
        /// <summary>
        /// 字段空返回0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int AdjustIntValue(string value)
        {
            int nValue = 0;
            Int32.TryParse(value, out nValue);
            return nValue;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string input)
        {
            return MD5Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string MD5Encrypt(string input, Encoding encode)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(encode.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static double GetTimeStamp()
        {
            return (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        }

        /// <summary>
        /// 初始化超时时间
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static TimeSpan InitTimeout(int seconds)
        {
            var totalSeconds = seconds * 10000000;
            return new TimeSpan(totalSeconds);
        }

        /// <summary>
        /// 获取最新流水号
        /// </summary>
        /// <param name="fixedNO">获取最新流水号</param>
        /// <returns></returns>
        public static string GetMaxNo(string fixedNO)
        {
            string flownum = fixedNO.Substring(fixedNO.Length - 4, 4);
            string numberstr = flownum.TrimStart('0');
            int flowno = 0;
            if (int.TryParse(numberstr, out flowno))
            {
                int LastestNo = flowno + 1;
                string padleftNO = LastestNo.ToString().PadLeft(4, '0');
                return padleftNO;
            }
            return string.Empty;
        }
    }
}
