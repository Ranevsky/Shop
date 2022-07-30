using AutoMapper;

using Shop.Models.Product;

namespace Shop.Profiles;

public sealed class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<ProductImage, ImageProductView>()
            .ForMember(imageView => imageView.Url, opt =>
            {
                opt.MapFrom(image => ImageProductUrl(image));
            });

        CreateMap<ProductImage?, string?>()
            .ConvertUsing(product => ImageProductUrl(product));
    }
    public static string? ImageProductUrl(ProductImage? image)
    {
        return image?.Url;
    }
}
