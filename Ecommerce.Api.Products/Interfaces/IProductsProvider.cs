using Ecommerce.Api.Products.Models;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductAsync();
        Task<(bool IsSuccess, Product product, string ErrorMessage)> GetProductByIdAsync(int id);
    }
}
