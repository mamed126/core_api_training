using auto_fac_demo2.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace auto_fac_demo2.Controllers
{
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;
        public TruckController(ITruckService truckService)
        {
            _truckService = truckService;
        }
        [HttpGet]
        [Route("api/truck/get")]
        public ActionResult GetAll()
        {
            var trucks = _truckService.GetAll();
            return Ok(trucks);
        }
    }
}