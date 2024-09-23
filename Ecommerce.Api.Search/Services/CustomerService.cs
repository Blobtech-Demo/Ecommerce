using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System.Text.Json;

namespace Ecommerce.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerByIdAsync(int custId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("CustomerService");
                var response = await httpClient.GetAsync($"api/customers/{custId}");
                if (response.IsSuccessStatusCode)
                {
                    var customer = await response.Content.ReadAsByteArrayAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<dynamic>(customer);
                    return(true, result, null);
                }
                return(false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return(false, null, ex.Message.ToString());
            }
        }
    }
}
