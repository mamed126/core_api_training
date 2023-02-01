namespace fluent_validation_demo.DataService
{
    public class TruckDataService
    {
        private IEnumerable<Dto.TruckDto> _data;

        public void AddDemoData()
        {
            var demoData = new List<Dto.TruckDto>();

            demoData.Add(new Dto.TruckDto { Brand = "Volvo", Id = 1, Model = "Volvo Fh 430" });
            demoData.Add(new Dto.TruckDto { Brand = "Volvo", Id = 2, Model = "Volvo Fh 460" });
            demoData.Add(new Dto.TruckDto { Brand = "Scania", Id = 3, Model = "S 400" });
            demoData.Add(new Dto.TruckDto { Brand = "Scania", Id = 4, Model = "S 500" });

            _data = demoData;
        }

        public TruckDataService()
        {
            AddDemoData();
        }

        public IEnumerable<Dto.TruckDto> GetAll()
        {
            return _data;
        }

        public void Add(Dto.TruckDto dto)
        {
            _data.Concat<Dto.TruckDto>(new[] { dto });
        }
    }
}
