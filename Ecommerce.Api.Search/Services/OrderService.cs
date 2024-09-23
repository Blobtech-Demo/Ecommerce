using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System.Text.Json;

namespace Ecommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger) 
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int CustomerId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("OrderService");
                var response = await httpClient.GetAsync($"api/orders/{CustomerId}");
                if (response.IsSuccessStatusCode)
                {
                    var orders = await response.Content.ReadAsByteArrayAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(orders, options);
                    return(true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message.ToString());
                return(false, null, ex.Message.ToString());
            }
        }
    }
}
