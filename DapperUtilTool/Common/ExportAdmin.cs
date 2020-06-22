using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Common
{
    public class ExportAdmin : ExportModel<ExportAdminItem>
    {
        ///public List<ExportAdminItem> Items { get; set; }
    }

    public class ExportAdminItem
    {
        [ExportField("用户名")]
        public string Username { get; set; }

        [ExportField("昵称", Align = FieldAlign.Left, Type = FieldType.String)]
        public string Nickname { get; set; }

        [ExportField("创建时间", Align = FieldAlign.Left, Type = FieldType.DateTime, Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        public DateTime CreateTime { get; set; }

        [ExportField("登录日期", Align = FieldAlign.Left, Type = FieldType.DateTime, Format = "yyyy-MM-dd", Width = 12)]
        public DateTime LoginTime { get; set; }

        [ExportField("金额", Align = FieldAlign.Right, Type = FieldType.Numeric, Format = "", Width = 10)]
        public decimal Money { get; set; }

        public int NoInclude { get; set; }
    }
}
