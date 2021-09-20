using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikroDataTransferAPI.Dto
{
    public class ProductInfoViewDto
    {
        public List<ProductInfoLine> Data { get; set; }
    }

    public class ProductInfoLine
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public double Price { get; set; }
        public double ProductBalance { get; set; }
        public string Barcode { get; set; }
    }
}
