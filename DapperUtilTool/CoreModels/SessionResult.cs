using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.CoreModels
{
    /// <summary>
    /// Session结果集
    /// </summary>
    [Serializable]
    public class SessionResult
    {
        /// <summary>
        /// 登陆用户Guid
        /// </summary>
        public  string LoginUseID { get; set; }
        /// <summary>
        /// 登陆用户名
        /// </summary>
        public  string LoginUseName { get; set; }
        /// <summary>
        /// 角色(财务：Finance  销售：Sale  其它：Other)
        /// </summary>
        public  string RoleName { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIP { get; set; }
        /// <summary>
        /// 请求URL
        /// </summary>
        public  string RequestUrl { get; set; }
    }
}
