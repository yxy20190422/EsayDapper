using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperModelCore.DBModel;
using DapperService;
using DapperService.IService;
using Kogel.Dapper.Extension.Model;
using DapperService.Dto;
using System.Threading;
using System.Net.Http;
using DapperUtilTool.Extensions;

namespace DapperWebAPI.Controllers
{
    /// <summary>
    /// 系统基本增删查改说明
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        #region 注入服务
        private IValuesServices _service;
        private readonly IHttpClientFactory _clientFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="clientFactory"></param>
        public ValuesController(IValuesServices service,IHttpClientFactory clientFactory)
        {
            _service = service;
            _clientFactory = clientFactory;
        }
        #endregion

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public int InsertEntity(ValuesInputDto input)
        {
            return _service.InsertEntity(input);
        }


        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public int InsertBatchEntity(ValuesInputDto input)
        {
            return _service.InsertBatchEntity(input);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public int UpdateEntity(ValuesInputDto input)
        {
            return _service.UpdateEntity(input);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public int DeleteEntity(ValuesInputDto input)
        {
            return _service.DeleteEntity(input);
        }

        /// <summary>
        /// 测试根据ID获取数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public Values Index(int ID)
        {
            return _service.RepositoryGetByID(ID);
        }
        /// <summary>
        /// 分页测试
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        public PageList<Values> GetPageList(int pageIndex,int pageSize)
        {
            return _service.RepositoryPageList(pageIndex, pageSize);
        }
        /// <summary>
        /// 获取指定列List的聚合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<ValuesOutputDto> GetList()
        {
            //var res = _clientFactory.GetMessageFromApiDress("get", "", "", "/api/Values/Get").Result;
            return _service.GetList();
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PageList<ValuesOutputDto> GetStockPageList(int pageIndex, int pageSize)
        {
            //var res = _clientFactory.GetMessageFromApiDress("get", "", "", "/api/Values/Get").Result;
            return _service.GetStockPageList(pageIndex, pageSize);
        }
    }
}