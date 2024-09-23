using Ecommerce.Api.Customers.Models;

namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool isSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, Customer Customer, string ErrorMessage)> GetCustomerByIdAsync(int id);
    }
}
