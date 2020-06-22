using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto
{
    /// <summary>
    /// 分页的公用实体类
    /// </summary>
    public class PageEntity
    {
        /// <summary>
        /// 每页几条数据
        /// </summary>
        public int PageSize = 50;
        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageIndex = 1;
    }
}
