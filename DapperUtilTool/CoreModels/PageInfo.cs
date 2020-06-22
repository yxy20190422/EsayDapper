using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.CoreModels
{/// <summary>
 /// 分页参数
 /// </summary>
    public class PageParam
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页码大小
        /// </summary>
        public int PageSize { get; set; } = 15;
    }
    /// <summary>
    /// 页码信息
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class PageInfo<T> : PageParam
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 返回数据集
        /// </summary>
        public List<T> ResultList { get; set; }

       

    }
}
