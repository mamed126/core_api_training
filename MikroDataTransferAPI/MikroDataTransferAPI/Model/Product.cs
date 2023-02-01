using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikroDataTransferAPI.Model
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 25, ErrorMessage = "Max len=25")]
        public string Code { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Max len=50")]
        public string Name { get; set; }
    }
}
