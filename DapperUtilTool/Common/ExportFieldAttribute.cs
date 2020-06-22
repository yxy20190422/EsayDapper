using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Common
{
    #region 导出的通用主体
    /// <summary>
    /// 
    /// </summary>
    public class ExportFieldAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title;
        /// <summary>
        /// 数据类型，默认是string
        /// </summary>
        public FieldType Type = FieldType.String;
        /// <summary>
        /// 内容在单元格的水平位置
        /// </summary>
        public FieldAlign Align = FieldAlign.Left;
        /// <summary>
        /// 内容的格式化输出
        /// </summary>
        public string Format = string.Empty;
        /// <summary>
        /// 字符数
        /// </summary>
        public int Width = 0;

        public ExportFieldAttribute(string title)
        {
            Title = title;
        }
    }
    #endregion
    public enum FieldType
    {
        String,
        Numeric,
        DateTime
    }

    public enum FieldAlign
    {
        Left,
        Center,
        Right
    }
}
