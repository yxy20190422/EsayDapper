using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.NLog.Connection
{/// <summary>
 /// 配置
 /// </summary>
    public class ExceptionslessOptions
    {

        private string _apikey;


        /// <summary>
        /// 连接的Exceptionsless项目Apikey
        /// </summary>
        public string Apikey
        {
            get
            {
                return _apikey;
            }
        }


        private string _serverUrl;

        /// <summary>
        /// 连接的Exceptionsless服务器地址
        /// </summary>
        public string ServerUrl
        {
            get
            {
                return _serverUrl;
            }
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="apikey">apikey</param>
        /// <param name="serverUrl">serverUrl</param>
        public ExceptionslessOptions(string apikey, string serverUrl)
        {
            _serverUrl = serverUrl;
            _apikey = apikey;
        }
    }
}
