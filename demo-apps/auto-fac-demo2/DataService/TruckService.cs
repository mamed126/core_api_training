using auto_fac_demo2.Dto;

namespace auto_fac_demo2.DataService
{
    public class TruckService : Abstraction.ITruckService
    {
        public IEnumerable<Truck> GetAll()
        {
            var trucks = new List<Truck>();

            trucks.Add(new Truck { Id = 1, Name = "Volvo F12" });
            trucks.Add(new Truck { Id = 2, Name = "Scania S400" });

            return trucks;
        }
    }
}
