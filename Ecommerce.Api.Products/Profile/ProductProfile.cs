namespace Ecommerce.Api.Products.Profile
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile() 
        {
            CreateMap<Db.Product, Models.Product>();
        }
    }
}
