using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperService.Dto;
using DapperUtilTool.CoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebAPI.Controllers
{
    #region 基础数据控制器
    /// <summary>
    /// 基础数据控制器
    /// </summary>
    [AllowAnonymous]
    public class BaseDataController : ApiControllerBase
    {
        /// <summary>
        /// DropDown:参数：negprice标记负差价price标记有价格平台
        /// </summary>
        /// <param name="DropDown">下拉框的名字</param>
        /// <returns></returns>
        [HttpGet]
        public OnlineOrdersResultDto<List<DropDownItem>> GetSelectBy(string DropDown)
        {
            OnlineOrdersResultDto<List<DropDownItem>> res = new OnlineOrdersResultDto<List<DropDownItem>>() { StatusCode = 0, Message = "获取下拉基础信息失败", Ext1 = new List<DropDownItem>() };
            Dictionary<int, string> result = new Dictionary<int, string>();
            switch (DropDown.ToLower())
            {
                case "negprice":
                    result = EnumCenter.GetDictionaryForEnum<EnumCenter.Ppc_StockInfo_NegativeStockType>();
                    break;
                case "price":
                    result = EnumCenter.GetDictionaryForEnum<EnumCenter.Ppc_StockInfo_PricePlatCount>();
                    break;
            }
            List<DropDownItem> dropdown = new List<DropDownItem>();          
            var listDropDown = result.ToList();
            foreach (var item in listDropDown)
            {
                DropDownItem down = new DropDownItem();
                down.key = item.Key;
                down.value = item.Value;
                dropdown.Add(down);
            }
            res.StatusCode = 1;
            res.Ext1 = dropdown;
            res.Message = "获取下拉基础信息成功";
            return res;
        }
    }
    #endregion

}