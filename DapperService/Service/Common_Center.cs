using DapperUtilTool.CoreModels;
using Kogel.Dapper.Extension.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Service
{
    /// <summary>
    /// 公共中心
    /// </summary>
    public static class Common_Center
    {
        /// <summary>
        /// 因前后端分离改为统一的分页实体(前端需要仅此而已没啥意义);
        /// </summary>
        /// <returns></returns>
        public static PageInfo<T> GetCommonPageEntityies<T>(this PageList<T> pageModel)
        {
            PageInfo<T> result = new PageInfo<T>();
            result.PageCount = pageModel.TotalPage;
            result.ResultList = pageModel.Items;
            result.TotalCount = pageModel.Total;
            result.PageIndex = pageModel.PageIndex;
            result.PageSize = pageModel.PageSize;
            return result;
        }

        #region 获取采购主管的属下员工

        #endregion
    }
}
