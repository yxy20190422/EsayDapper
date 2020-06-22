using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Common
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
    }
}
