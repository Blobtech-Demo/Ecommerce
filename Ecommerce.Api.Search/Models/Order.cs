﻿namespace Ecommerce.Api.Search.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public Decimal Total { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
