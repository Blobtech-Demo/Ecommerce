using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider: IProductsProvider
    {
        private readonly ProductsDbContext productsDbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;
        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper) 
        {
            this.productsDbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!productsDbContext.Products.Any())
            {
                productsDbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                productsDbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 150 });
                productsDbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 200 });
                productsDbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 90 });
                productsDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductAsync()
        {
            try
            {
                var products = await productsDbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "No Result Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await productsDbContext.Products.FindAsync(id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }
                
                return (false, null, "No Result Found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.Message.ToString());
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
