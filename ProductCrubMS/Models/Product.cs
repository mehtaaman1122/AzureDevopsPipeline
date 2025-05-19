using System.ComponentModel.DataAnnotations.Schema;
namespace ProductCrubMS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
    }
}
