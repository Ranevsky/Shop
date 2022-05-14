using AutoMapper;
using Shop.Models.Converters;
using Shop.Models.View;

namespace Shop.Models;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Product, ProductView>();

        CreateMap<Image, ImageView>()
            .ForMember(i => i.Url, opt =>
            {
                opt.MapFrom(i => $"{Program.applicationUrl}{Program.ImageUrl}/{i.Name}");
            });
    }
}