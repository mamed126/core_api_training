using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikroDataTransferAPI.Contracts;
using MikroDataTransferAPI.Dto;
using System;

namespace MikroDataTransferAPI.Controllers
{
    [Route("api/query")]
    [ApiController]
    public class QueryController:ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repoWrapper;
        public QueryController(ILoggerManager logger,IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
        }
        [Authorize]
        [HttpGet("getProductInfo")]
        public IActionResult GetProductInfo([FromBody]ProductInfoGetActParam p)
        {
            try
            {
                var productRepo = _repoWrapper.ProductRepo;

                var productInfoLines = productRepo.GetProductInfoLines(p);

                var result = new ProductInfoViewDto
                {
                    Data = productInfoLines
                };

                return Ok(result);
            }
            catch(Exception exp)
            {
                string msg = "Internal server error,on GetProductInfo action...";
                _logger.LogError(msg + " :: " + exp.Message);
                return StatusCode(500, msg);
            }
        }
    }
}
