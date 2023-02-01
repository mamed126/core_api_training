using Dapper;
using MikroDataTransferAPI.Contracts;
using MikroDataTransferAPI.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MikroDataTransferAPI.Repositories
{
    public class ProductRepository2 : IProductRepository2
    {
        private readonly string _conn;
        public ProductRepository2(string conn)
        {
            _conn = conn;
        }
        public IEnumerable<Product> GetProducts(PaginationModel m)
        {
            using (var conn = new SqlConnection(_conn))
            {
                var data = conn.Query<Product>("select * from Products",
                    commandTimeout: 0, commandType: System.Data.CommandType.Text);

                return data.OrderBy(on => on.Name).Skip((m.PageNumber - 1) * m.PageSize).Take(m.PageSize).ToList();
            }
        }
    }
}
