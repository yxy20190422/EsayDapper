using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.Common
{
    public class ExportModel<T>
    {
        /// <summary>
        /// 导出的文件第一行的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 导出的文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 显示的筛选条件，文件的第二行
        /// </summary>
        public string FilterContent { get; set; }

        /// <summary>
        /// 工作簿名称
        /// </summary>
        public string SheetName { get; set; }

        public List<T> Items { get; set; }
    }
}
