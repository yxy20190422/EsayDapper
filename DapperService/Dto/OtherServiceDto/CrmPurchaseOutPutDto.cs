using System;
using System.Collections.Generic;
using System.Text;

namespace DapperService.Dto.OtherServiceDto
{
    /// <summary>
    /// 返回CRM的下属
    /// </summary>
    public class CrmPurchaseOutPutDto
    {
        /// <summary>
        /// crm账号ID
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EName { get; set; }
    }
}
