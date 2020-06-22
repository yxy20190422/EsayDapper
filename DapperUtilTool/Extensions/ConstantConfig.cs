using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Extensions
{/// <summary>
 /// 常量配置
 /// </summary>
    public class ConstantConfig
    {
        /// <summary>
        /// 日志键
        /// </summary>
        public const string _NlogKey = "OnActionNLogKey";

        /// <summary>
        /// Session键
        /// </summary>
        public const string _SessionKey = "OnActionSessionKey";

        /// <summary>
        /// 默认SqlServer数据库连接
        /// </summary>
        public const string _MsSqlLocalConnectionString = @"Server=.;Database=TestDB;Trusted_Connection=True;ConnectRetryCount=0";

        /// <summary>
        /// 默认MySql数据库连接
        /// </summary>
        public const string _MySqlLocalConnectionString = @"server=192.168.112.129;post=3306;database=testdb;user=root;password=sa123;";

        /// <summary>
        /// 默认MySql数据库连接
        /// </summary>
        public const string _MySqlConnectionString = @"server=rm-wz9li05w3782te7e72o.mysql.rds.aliyuncs.com;port=3306;database=preorder;user=adm_preorder;password=adm0423#preorder;";

        /// <summary>
        /// 键
        /// </summary>
        public const string Key = "7355e1b7-e956-42b6-a64c-61ff6ff04748";

        /// <summary>
        /// 密钥
        /// </summary>
        public const string Secret = "19adfe4a-22cb-11e9-896d-7cd30ae43954";
    }
}
