using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Extensions
{
    /// <summary>
    /// Session信息扩展方法
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="session">session</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="session">session</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
