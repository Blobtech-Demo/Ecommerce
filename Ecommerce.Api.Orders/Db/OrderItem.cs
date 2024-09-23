using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Orders.Db
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int  Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
