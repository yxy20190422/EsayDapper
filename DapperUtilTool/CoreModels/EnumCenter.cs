using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DapperUtilTool.CoreModels
{
    #region 枚举中心
    /// <summary>
    /// 枚举值
    /// </summary>
    public class EnumCenter
    {
        public static int GetValueForEnum<T>(string descrinption)
        {
            int value = 0;
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if (GetDescription(item) == descrinption)
                {
                    return (int)item;
                }
            }
            return value;
        }
        /// <summary> 
        /// 获取枚举信息
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>  
        public static Dictionary<int, string> GetDictionaryForEnum<T>()
        {
            Dictionary<int, string> dics = new Dictionary<int, string>();
            foreach (var v in Enum.GetValues(typeof(T)))
                dics.Add((int)v, GetDescription(v));
            return dics;
        }

        #region 调用方法
        /// <summary>
        /// 获取枚举Description Attribute的值
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetDescription(object value, string unknownDescription = "")
        {
            if (value == null)
            {
                return !string.IsNullOrEmpty(unknownDescription) ? unknownDescription : string.Empty;
            }
            var field = value.GetType().GetField(value.ToString());

            if (field == null)
            {
                return !string.IsNullOrEmpty(unknownDescription) ? unknownDescription : string.Empty;
            }

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// 获取枚举Description Attribute的值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetDescription<T>(int? value, string unknownDescription = "")
        {
            if (value == null)
            {
                return !string.IsNullOrEmpty(unknownDescription) ? unknownDescription : string.Empty;
            }
            return GetDescription((T)System.Enum.Parse(typeof(T), value.Value.ToString()), unknownDescription);
        }
        /// <summary>
        /// 获取枚举Description Attribute的值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <param name="unknownDescription"></param>
        /// <returns></returns>
        public static string GetDescription<T>(string value, string unknownDescription = "")
        {
            if (value == null)
            {
                return !string.IsNullOrEmpty(unknownDescription) ? unknownDescription : string.Empty;
            }
            if (!Enum.GetNames(typeof(T)).Contains(value))
            {
                return value;
            }
            return GetDescription((T)System.Enum.Parse(typeof(T), value), unknownDescription);
        }



        #endregion

        #region 比价物料主表的上传物料明细的状态的枚举
        /// <summary>
        /// 比价物料主表的上传物料明细的状态的枚举
        /// </summary>
        public enum Ppc_StockInfo_Status
        {
            /// <summary>
            /// 无任务状态
            /// </summary>
            [Description("无任务状态")]
            NoTask = 0,
            /// <summary>
            /// 无任务状态
            /// </summary>
            [Description("与接口对接中")]
            InterfaceDocking = 1,
            /// <summary>
            /// 无任务状态
            /// </summary>
            [Description("自查询处理中")]
            SelfSearching = 2
        }
        #endregion

        #region 负价差物料的枚举
        /// <summary>
        /// 负价差物料
        /// </summary>
        public enum Ppc_StockInfo_NegativeStockType
        {
            /// <summary>
            /// 单独展示
            /// </summary>
            [Description("单独展示")]
            SepDisplay = 1,
            /// <summary>
            /// 无任务状态
            /// </summary>
            [Description("不单独展示")]
            NoSepDisplay = 2
        }
        #endregion

        #region 有价平台数量
        /// <summary>
        /// 有价平台数量
        /// </summary>
        public enum Ppc_StockInfo_PricePlatCount
        {
            /// <summary>
            /// 1个
            /// </summary>
            [Description("1个")]
            OnePlatCount = 1,
            /// <summary>
            /// 2个或2个以上
            /// </summary>
            [Description("2个或2个以上")]
            TwoMorePlatCount = 2
        }
        #endregion


        #region 比价物料主表的上传主表的状态的枚举
        /// <summary>
        /// 比价物料主表的上传物料明细的状态的枚举
        /// </summary>
        public enum Ppc_UploadInfo_Status
        {
            /// <summary>
            /// 无任务
            /// </summary>
            [Description("无任务")]
            NoTask = 0,
            /// <summary>
            /// 待对比
            /// </summary>
            [Description("待对比")]
            InterfaceDocking = 1,
            /// <summary>
            /// 对比中
            /// </summary>
            [Description("对比中")]
            SelfSearching = 2
        }
        #endregion
    }
    #endregion
}
