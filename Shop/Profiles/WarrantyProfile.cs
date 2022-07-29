using AutoMapper;

using Shop.Models.Product;

namespace Shop.Profiles;

public class WarrantyProfile : Profile
{
    public WarrantyProfile()
    {
        // WarrantyAddModel
        CreateMap<WarrantyAddModel, Warranty>();
    }
}
