using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Products.Db
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Inventory {  get; set; } 
    }
}
