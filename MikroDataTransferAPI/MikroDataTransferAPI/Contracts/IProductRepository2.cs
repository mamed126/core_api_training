using MikroDataTransferAPI.Model;
using System.Collections.Generic;

namespace MikroDataTransferAPI.Contracts
{
    public interface IProductRepository2
    {
        IEnumerable<Product> GetProducts(PaginationModel m);
    }
}
