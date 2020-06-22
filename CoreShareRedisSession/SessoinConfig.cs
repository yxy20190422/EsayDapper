using System;
using System.Collections.Generic;
using System.Text;

namespace CoreShareRedisSession
{
    public class SessoinConfig
    {
        /// <summary>
        /// session过期时长，分钟
        /// </summary>
        public int SessionTimeOut { get; set; }

        /// <summary>
        /// 实例库索引
        /// </summary>
        public RedisCacheConfigData RedisCacheConfig { get; set; }
        /// <summary>
        /// Session保存在客户端的名字
        /// </summary>
        public string SessionCookieName { get; set; }
        /// <summary>
        /// SessionId对应的Cookie保存在域名
        /// </summary>
        public string SessionCookieDomain { get; set; }
    }
}
