using DapperUtilTool.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Export
{
    /// <summary>
    /// 库存上传导出实体
    /// </summary>
    public class StockUploadModel
    {
        /// <summary>
        /// 型号
        /// </summary>
        [ExportField("*型号")]
        public string Model { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [ExportField("*品牌")]
        public string Brand { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [ExportField("批次")]
        public string BatchNo { get; set; }
        /// <summary>
        /// 封装
        /// </summary>
        [ExportField("封装")]
        public string Encapsulation { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [ExportField("描述")]
        public string Remark { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        [ExportField("库存")]
        public int? InvQty { get; set; }
        /// <summary>
        /// SPQ
        /// </summary>
        [ExportField("*SPQ")]
        public int? SPQ { get; set; }
        /// <summary>
        /// MOQ
        /// </summary>
        [ExportField("*MOQ")]
        public int? MOQ { get; set; }

        /// <summary>
        /// *货期(SZ)
        /// </summary>
        [ExportField("*货期(SZ)")]
        public string SZDelivery { get; set; }
        /// <summary>
        /// 货期(HK)
        /// </summary>
        [ExportField("货期(HK)")]
        public string HKDelivery { get; set; }
        /// <summary>
        /// 区间数量1
        /// </summary>
        [ExportField("区间数量1")]
        public int? AreaQty1 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)1
        /// </summary>
        [ExportField("区间采购单价(CNY)1")]
        public decimal? AreaPurchaseUnitPriceCNY1 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)1
        /// </summary>
        [ExportField("区间采购单价(USD)1")]
        public decimal? AreaPurchaseUnitPriceUSD1 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)1
        /// </summary>
        [ExportField("区间销售单价(CNY)1")]
        public decimal? AreaSalesPriceCNY1 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)1
        /// </summary>
        [ExportField("区间销售单价(USD)1")]
        public decimal? AreaSalesPriceUSD1 { get; set; }
        /// <summary>
        /// 区间数量2
        /// </summary>
        [ExportField("区间数量2")]
        public int? AreaQty2 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)2
        /// </summary>
        [ExportField("区间采购单价(CNY)2")]
        public decimal? AreaPurchaseUnitPriceCNY2 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)2
        /// </summary>
        [ExportField("区间采购单价(USD)2")]
        public decimal? AreaPurchaseUnitPriceUSD2 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)2
        /// </summary>
        [ExportField("区间销售单价(CNY)2")]
        public decimal? AreaSalesPriceCNY2 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)2
        /// </summary>
        [ExportField("区间销售单价(USD)2")]
        public decimal? AreaSalesPriceUSD2 { get; set; }
        /// <summary>
        /// 区间数量3
        /// </summary>
        [ExportField("区间数量3")]
        public int? AreaQty3 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)3
        /// </summary>
        [ExportField("区间采购单价(CNY)3")]
        public decimal? AreaPurchaseUnitPriceCNY3 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)3
        /// </summary>
        [ExportField("区间采购单价(USD)3")]
        public decimal? AreaPurchaseUnitPriceUSD3 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)3
        /// </summary>
        [ExportField("区间销售单价(CNY)3")]
        public decimal? AreaSalesPriceCNY3 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)3
        /// </summary>
        [ExportField("区间销售单价(USD)3")]
        public decimal? AreaSalesPriceUSD3 { get; set; }
        /// <summary>
        /// 区间数量4
        /// </summary>
        [ExportField("区间数量4")]
        public int? AreaQty4 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)4
        /// </summary>
        [ExportField("区间采购单价(CNY)4")]
        public decimal? AreaPurchaseUnitPriceCNY4 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)4
        /// </summary>
        [ExportField("区间采购单价(USD)4")]
        public decimal? AreaPurchaseUnitPriceUSD4 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)4
        /// </summary>
        [ExportField("区间销售单价(CNY)4")]
        public decimal? AreaSalesPriceCNY4 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)4
        /// </summary>
        [ExportField("区间销售单价(USD)4")]
        public decimal? AreaSalesPriceUSD4 { get; set; }
        /// <summary>
        /// 区间数量5
        /// </summary>
        [ExportField("区间数量5")]
        public int? AreaQty5 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)5
        /// </summary>
        [ExportField("区间采购单价(CNY)5")]
        public decimal? AreaPurchaseUnitPriceCNY5 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)5
        /// </summary>
        [ExportField("区间采购单价(USD)5")]
        public decimal? AreaPurchaseUnitPriceUSD5 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)5
        /// </summary>
        [ExportField("区间销售单价(CNY)5")]
        public decimal? AreaSalesPriceCNY5 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)5
        /// </summary>
        [ExportField("区间销售单价(USD)5")]
        public decimal? AreaSalesPriceUSD5 { get; set; }
        /// <summary>
        /// 区间数量6
        /// </summary>
        [ExportField("区间数量6")]
        public int? AreaQty6 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)6
        /// </summary>
        [ExportField("区间采购单价(CNY)6")]
        public decimal? AreaPurchaseUnitPriceCNY6 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)6
        /// </summary>
        [ExportField("区间采购单价(USD)6")]
        public decimal? AreaPurchaseUnitPriceUSD6 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)6
        /// </summary>
        [ExportField("区间销售单价(CNY)6")]
        public decimal? AreaSalesPriceCNY6 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)6
        /// </summary>
        [ExportField("区间销售单价(USD)6")]
        public decimal? AreaSalesPriceUSD6 { get; set; }
        /// <summary>
        /// 区间数量7
        /// </summary>
        [ExportField("区间数量7")]
        public int? AreaQty7 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)7
        /// </summary>
        [ExportField("区间采购单价(CNY)7")]
        public decimal? AreaPurchaseUnitPriceCNY7 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)7
        /// </summary>
        [ExportField("区间采购单价(USD)7")]
        public decimal? AreaPurchaseUnitPriceUSD7 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)7
        /// </summary>
        [ExportField("区间销售单价(CNY)7")]
        public decimal? AreaSalesPriceCNY7 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)7
        /// </summary>
        [ExportField("区间销售单价(USD)7")]
        public decimal? AreaSalesPriceUSD7 { get; set; }
        /// <summary>
        /// 区间数量8
        /// </summary>
        [ExportField("区间数量8")]
        public int? AreaQty8 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)8
        /// </summary>
        [ExportField("区间采购单价(CNY)8")]
        public decimal? AreaPurchaseUnitPriceCNY8 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)8
        /// </summary>
        [ExportField("区间采购单价(USD)8")]
        public decimal? AreaPurchaseUnitPriceUSD8 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)8
        /// </summary>
        [ExportField("区间销售单价(CNY)8")]
        public decimal? AreaSalesPriceCNY8 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)8
        /// </summary>
        [ExportField("区间销售单价(USD)8")]
        public decimal? AreaSalesPriceUSD8 { get; set; }
        /// <summary>
        /// 区间数量9
        /// </summary>
        [ExportField("区间数量9")]
        public int? AreaQty9 { get; set; }
        /// <summary>
        /// 区间采购单价(CNY)9
        /// </summary>
        [ExportField("区间采购单价(CNY)9")]
        public decimal? AreaPurchaseUnitPriceCNY9 { get; set; }
        /// <summary>
        /// 区间采购单价(USD)9
        /// </summary>
        [ExportField("区间采购单价(USD)9")]
        public decimal? AreaPurchaseUnitPriceUSD9 { get; set; }
        /// <summary>
        /// 区间销售单价(CNY)9
        /// </summary>
        [ExportField("区间销售单价(CNY)9")]
        public decimal? AreaSalesPriceCNY9 { get; set; }
        /// <summary>
        /// 区间销售单价(USD)9
        /// </summary>
        [ExportField("区间销售单价(USD)9")]
        public decimal? AreaSalesPriceUSD9 { get; set; }
        /// <summary>
        /// 有效期(月)
        /// </summary>
        [ExportField("有效期(月)")]
        public int? VaildMonth { get; set; }
    }
}
