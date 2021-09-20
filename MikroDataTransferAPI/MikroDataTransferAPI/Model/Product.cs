using System.ComponentModel.DataAnnotations.Schema;

namespace MikroDataTransferAPI.Model
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string  Name { get; set; }
    }
}
