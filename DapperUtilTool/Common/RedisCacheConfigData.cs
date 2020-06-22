using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Common
{
    public class RedisCacheConfigData
    {
        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Redis实例名称
        /// </summary>
        public string InstanceName { get; set; }
    }
}
