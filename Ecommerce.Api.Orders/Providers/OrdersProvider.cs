using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper; 

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedOrderItems();
            SeedOrders();
            
        }

        private void SeedOrders()
        {
            if(!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 1,
                    CustomerId = 3,
                    OrderDate = new DateTime(),
                    Items = dbContext.OrderItems.Where(o => o.OrderId == 1).ToList(),
                    Total = 25
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = new DateTime(),
                    Items = dbContext.OrderItems.Where(o => o.OrderId == 2).ToList(),
                    Total = 315
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 3,
                    CustomerId = 4,
                    OrderDate = new DateTime(),
                    Items = dbContext.OrderItems.Where(o => o.OrderId == 3).ToList(),
                    Total = 120
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 4,
                    CustomerId = 1,
                    OrderDate = new DateTime(),
                    Items = dbContext.OrderItems.Where(o => o.OrderId == 4).ToList(),
                    Total = 5
                });
                dbContext.SaveChanges();

            }
        }

        private void SeedOrderItems()
        {
            if (!dbContext.OrderItems.Any())
            {
                dbContext.OrderItems.Add( new Db.OrderItem() { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 10 } );
                dbContext.OrderItems.Add(new Db.OrderItem() { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 5 });
                dbContext.OrderItems.Add(new Db.OrderItem() { Id = 3, OrderId = 2, ProductId = 3, Quantity = 3, UnitPrice = 100 });
                dbContext.OrderItems.Add(new Db.OrderItem() { Id = 4, OrderId = 2, ProductId = 4, Quantity = 1, UnitPrice = 15 });
                dbContext.OrderItems.Add(new Db.OrderItem() { Id = 5, OrderId = 3, ProductId = 1, Quantity = 2, UnitPrice = 10 });
                dbContext.OrderItems.Add(new Db.OrderItem() { Id = 6, OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 });
                dbContext.OrderItems.Add(new Db.OrderItem() { Id = 7, OrderId = 4, ProductId = 2, Quantity = 1, UnitPrice = 5 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrderAsync(int CustomerId)
        {
            try
            {
                var orders = await dbContext.Orders.Include(o => o.Items).
                    Where(o => o.CustomerId == CustomerId).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "No Result Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message.ToString());
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
