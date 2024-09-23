using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System.Text.Json;

namespace Ecommerce.Api.Search.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProductService> logger;

        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ProductService");
                var response = await httpClient.GetAsync($"api/products");
                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadAsByteArrayAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(products, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message.ToString());
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
