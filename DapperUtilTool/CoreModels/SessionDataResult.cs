using DapperUtilTool.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.CoreModels
{
    /// <summary>
    /// Session数据结果集
    /// </summary>
    public class SessionDataResult
    {
        /// <summary>
        /// Session
        /// </summary>
        private static SessionResult _session
        {
            get
            {
                var session = HttpContextAccessorExtensions.Current.Session.Get<SessionResult>(ConstantConfig._SessionKey);
                if (session == null)
                {
                    session = new SessionResult();
                }
                return session;
            }
        }
        /// <summary>
        /// 当前登录人ID
        /// </summary>
        public static string LoginUseID
        {
            get
            {
                return _session.LoginUseID;
            }
        }
        /// <summary>
        /// 当前登录人ID
        /// </summary>
        public static string LoginUseName
        {
            get
            {
                return _session.LoginUseName;
            }
        }

        /// <summary>
        /// 角色(财务：Finance  销售：Sale  其它：Other)
        /// </summary>
        public static string RoleName
        {
            get
            {
                return _session.RoleName;
            }
        }

        /// <summary>
        /// 请求URL
        /// </summary>
        public static string RequestUrl
        {
            get
            {
                return _session.RequestUrl;
            }
        }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public static string ClientIP
        {
            get
            {
                return _session.ClientIP;
            }
        }
    }
}
