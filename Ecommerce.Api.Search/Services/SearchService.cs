using Ecommerce.Api.Search.Interfaces;


namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService; 
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int CustomerId)
        {
            var orderResult = await orderService.GetOrdersAsync(CustomerId);
            var productResult = await productService.GetProductsAsync();
            var customerResult = await customerService.GetCustomerByIdAsync(CustomerId);
            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ?
                            productResult.products?.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product Information is Not Available";
                    }
                }
                var orders = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer : new { Name = "Customer Information Not Available" },
                    Order = orderResult.Orders
                };
                return (true, orders);
            }
            return (false, null);
        }
    }
}
