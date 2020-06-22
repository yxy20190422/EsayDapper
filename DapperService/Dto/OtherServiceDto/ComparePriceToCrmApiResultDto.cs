using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto.OtherServiceDto
{
    /// <summary>
    /// 比价系统调用CRM系统返回采购及其所有下属
    /// </summary>
    public class ComparePriceToCrmApiResultDto<T>
    {
        /// <summary>
        ///返回接口状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 接口返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回的值
        /// </summary>
        public T ReValue { get; set; }
    }
}
