using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Agnibh Chandra", Address = "Baguiati" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Aparajita Sarkar", Address = "Bablatala" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Sampa Chandra", Address = "Baguiati" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Mousumi Sarkar", Address = "Bablatala" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FindAsync(id);
                if(customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex) {
                logger.LogError(ex.Message.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message.ToString());
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
