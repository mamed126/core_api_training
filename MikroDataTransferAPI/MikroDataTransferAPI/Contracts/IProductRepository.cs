using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MikroDataTransferAPI.Dto;

namespace MikroDataTransferAPI.Contracts
{
    public interface IProductRepository
    {
        List<ProductInfoLine> GetProductInfoLines(ProductInfoGetActParam p);
    }
}
