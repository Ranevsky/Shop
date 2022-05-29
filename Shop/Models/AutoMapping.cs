using AutoMapper;

using Shop.Models.View;

namespace Shop.Models;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Product, ProductView>()
            .ForMember(p => p.ProductType, opt =>
            {
                opt.MapFrom(p => p.Type.Name);
            })
            .ForMember(p => p.Warranty, opt =>
            {
                opt.MapFrom(p => Convert.WarrantyToString(p.Warranty));
            });


        CreateMap<Product, ProductCatalogView>()
            .ForMember(p => p.Image, opt =>
            {
                opt.MapFrom(p => Convert.FirstImageToString(p.Images));
            })
            .ForMember(p => p.ProductType, opt =>
             {
                 opt.MapFrom(p => p.Type.Name);
             });



        CreateMap<Image, ImageView>()
            .ForMember(i => i.Url, opt =>
            {
                opt.MapFrom(i => Convert.ImageToString(i));
            });
    }
    internal static class Convert
    {
        internal static string? FirstImageToString(IEnumerable<Image> images)
        {
            Image? image = images.FirstOrDefault();

            return image == null ? null : ImageToString(image);

        }
        internal static string ImageToString(Image image)
        {
            return $"{Program.applicationUrl}{Program.ImageUrl}/{image.Name}";

        }
        internal static string? WarrantyToString(Warranty? warranty)
        {
            return warranty?.Description ?? null;
        }

    }
}