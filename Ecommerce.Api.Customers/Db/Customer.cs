using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Customers.Db
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
