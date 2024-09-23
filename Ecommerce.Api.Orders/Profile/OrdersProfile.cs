namespace Ecommerce.Api.Orders.Profile
{
    public class OrdersProfile: AutoMapper.Profile
    {
        public OrdersProfile()
        {
            CreateMap<Db.OrderItem, Models.OrderItem>();
            CreateMap<Db.Order, Models.Order>();
        }
    }
}
