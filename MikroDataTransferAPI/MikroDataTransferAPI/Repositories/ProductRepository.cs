using Dapper;
using MikroDataTransferAPI.Contracts;
using MikroDataTransferAPI.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MikroDataTransferAPI.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly string _connString;

        public ProductRepository(string connString)
        {
            this._connString = connString;
        }

        public List<ProductInfoLine> GetProductInfoLines(ProductInfoGetActParam p)
        {
            using(var conn=new SqlConnection(this._connString))
            {
                conn.Open();

                string query = @"select * from 
                                 dbo._fnGetProductInfo(
                                 @Date,
                                 @Warehouses,
                                 @Products)";

                var data = conn.Query<ProductInfoLine>(query, 
                    p,
                    commandType: System.Data.CommandType.Text,
                    commandTimeout: 0);

                if (data == null)
                    return new List<ProductInfoLine>();
                
                return data.ToList();
            }
        }
    }
}
