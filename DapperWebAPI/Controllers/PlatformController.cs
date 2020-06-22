using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DapperModelCore.DapperCore;
using DapperService.Dto;
using DapperService.Dto.OtherServiceDto;
using DapperService.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebAPI.Controllers
{
    /// <summary>
    /// 平台控制器
    /// </summary>
    [AllowAnonymous]
    public class PlatformController : ApiControllerBase
    {
        /// <summary>
        /// 仓储接口
        /// </summary>
        private IRepository _iRepository { get; set; }
        /// <summary>
        /// 物料的服务
        /// </summary>
        private IPlatformServices _service { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>       
        public PlatformController(IPlatformServices services)
        {
            _service = services;
        }
        /// <summary>
        /// 获取所有的比价平台的名称与ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public OnlineOrdersResultDto<List<Ppc_PlatformConfigOutPutDto>> GetPlatform()
        {
            return _service.GetPlatform();
        }

        /// <summary>
        /// 获取所有的比价平台的名称与ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public OnlineOrdersResultDto<List<Ppc_PlatformConfigOutPutDto>> GetPlatform(PurchaseInputDto input)
        {
            input.CRMUserId = "17b2269a-0bc6-4a20-95ed-25b3386250b1";
             _service.GetPurchaseBelongsFromCrm1(input);
            return new OnlineOrdersResultDto<List<Ppc_PlatformConfigOutPutDto>>();
        }
    }
}