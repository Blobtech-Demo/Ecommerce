using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profile;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products.Test
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .EnableSensitiveDataLogging()
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnPoductByValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .EnableSensitiveDataLogging()
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductByIdAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnPoductByInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .EnableSensitiveDataLogging()
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductByIdAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.ErrorMessage);
        }
        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * (3.14))
                });
            }
            dbContext.SaveChanges();
        }
    }
}

   
