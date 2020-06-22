using NPOI.OpenXmlFormats.Dml.Diagram;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DapperUtilTool.UpLoadHead
{
    /// <summary>
    /// 所有上传的回到前端的表头展现形式
    /// </summary>
    public static class ConstUpLoadDic
    {
        /// <summary>
        /// 比价上传的字典模型
        /// </summary>
        public static Dictionary<string, string> StockHead = new Dictionary<string, string>() {
            {"型号", "型号"},
            {"品牌", "品牌"},
            {"库存", "库存"},
            {"SPQ", "SPQ"},
            {"MOQ", "MOQ"},
            {"供应商报价", "供应商报价"},
            {"批次", "批次"},
            {"封装", "封装"},
            {"描述", "描述"},
            {"货期(SZ)", "货期(SZ)"},
            {"货期(HK)", "货期(HK)"},
            {"阶梯","阶梯" }
        };

        public static List<DapperUtilTool.CoreModels.UpLoadHead> GetUpLoadHead(this Dictionary<string, string> dicHead)
        {
            List<DapperUtilTool.CoreModels.UpLoadHead> HeadList = new List<CoreModels.UpLoadHead>();
            foreach (var item in dicHead)
            {
                DapperUtilTool.CoreModels.UpLoadHead model = new CoreModels.UpLoadHead();
                model.Key = item.Key;
                model.Value = item.Value;
                HeadList.Add(model);
            }
            return HeadList;
        }
    }
}
