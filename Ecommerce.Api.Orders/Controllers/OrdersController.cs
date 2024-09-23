using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController: ControllerBase
    {
        private readonly IOrdersProvider _ordersProvider;
        public OrdersController(IOrdersProvider ordersProvider) 
        {
            _ordersProvider = ordersProvider;
        }

        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetOrdersAsync(int CustomerId)
        {
            var result = await _ordersProvider.GetOrderAsync(CustomerId);
            if(result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
