using Microsoft.AspNetCore.Mvc;

namespace fluent_validation_demo.Controllers
{
    [ApiController]
    public class TruckController : ControllerBase
    {
        [HttpGet]
        [Route("api/truck/get")]
        public ActionResult GetAll()
        {
            var service = new DataService.TruckDataService();
            var data = service.GetAll();
            return Ok(data);
        }

        [HttpPost]
        [Route("api/truck/add")]
        public ActionResult Add(Dto.TruckDto truck)
        {
            var service = new DataService.TruckDataService();
            service.Add(truck);
            return Ok(truck);
        }


    }
}
