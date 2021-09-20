using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikroDataTransferAPI.Contracts;
using MikroDataTransferAPI.Model;
using System;
using System.Collections.Generic;

namespace MikroDataTransferAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController:ControllerBase
    {
        private ILoggerManager _logger;
        public TestController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetData()
        {
            try
            {
                List<TestData> items = new List<TestData>();
                items.Add(new TestData { Id = 1, Name = "test1" });
                items.Add(new TestData { Id = 2, Name = "test2" });
                return Ok(items);
            }
            catch(Exception exp)
            {
                _logger.LogError($"Something went wrong inside GetAllItems action: {exp.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
