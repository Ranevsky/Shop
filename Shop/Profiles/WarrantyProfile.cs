using AutoMapper;

using Shop.Models.Product;

namespace Shop.Profiles;

public class WarrantyProfile : Profile
{
    public WarrantyProfile()
    {
        CreateMap<Warranty, WarrantyCountModel>()
         .ForMember(countModel => countModel.Count, opt =>
         {
             opt.MapFrom(warranty => warranty.Products.Count);
         });

        CreateMap<WarrantyAddModel, Warranty>();
    }
}
