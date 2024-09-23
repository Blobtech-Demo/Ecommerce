using Ecommerce.Api.Orders.Models;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrderAsync(int CustomerId);
    }
}
