using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Orders.Db
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public Decimal Total { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
