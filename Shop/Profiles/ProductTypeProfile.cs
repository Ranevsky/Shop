using AutoMapper;

using Shop.Models.Product;

namespace Shop.Profiles;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        // ProductTypeAddModel
        CreateMap<ProductTypeAddModel, ProductType>();
    }
}
