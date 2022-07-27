using AutoMapper;

using Shop.Models.Catalog;
using Shop.Models.Product;

namespace Shop.Profiles;

public sealed class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Product
        CreateMap<Product, ProductView>()
            .ForMember(p => p.ProductType, opt =>
            {
                opt.MapFrom(p => p.Type.Name);
            })
            .ForMember(p => p.Description, opt =>
            {
                opt.MapFrom(p => p.Description == null ? null : p.Description.Text);
            })
            .ForMember(p => p.Characteristic, opt =>
            {
                opt.MapFrom(p => p.Characteristics);
            });

        CreateMap<Image, ImageProductView>()
            .ForMember(imageView => imageView.Url, opt =>
            {
                opt.MapFrom(image => ImageGetUrl(image));
            });

        CreateMap<Warranty, ProductWarrantyView>();

        CreateMap<Image, string>()
            .ConvertUsing(image => ImageGetUrl(image));

        CreateMap<Characteristic, string>()
            .ConvertUsing(p => p.Description);


        CreateMap<Product, ProductInCatalogView>()
            .ForMember(productView => productView.ProductType, opt =>
             {
                 opt.MapFrom(product => product.Type.Name);
             })
            .ForMember(ProductView => ProductView.Image, opt =>
            {
                opt.MapFrom(product => product.Images.FirstOrDefault());
            });

        // ProductAddModel
        CreateMap<ProductAddModel, Product>();

        CreateMap<string, ProductType>()
            .ForMember(p => p.Name, opt =>
            {
                opt.MapFrom(s => s);
            });

        CreateMap<string, Description>()
            .ForMember(d => d.Text, opt =>
            {
                opt.MapFrom(s => s);
            });

        CreateMap<string, Characteristic>()
            .ForMember(c => c.Description, opt =>
            {
                opt.MapFrom(s => s);
            });

        CreateMap<string, Warranty>()
            .ForMember(w => w.Description, opt =>
            {
                opt.MapFrom(s => s);
            });

        // ProductType
        CreateMap<ProductTypeAddModel, ProductType>();
        CreateMap<ProductType, ProductTypeCountModel>()
            .ForMember(countModel => countModel.Count, opt =>
            {
                opt.MapFrom(type => type.Products.Count);
            });
    }

    public static string ImageGetUrl(Image image)
    {
        return $"{Program.ApplicationUrl}{Program.ImageUrl}{image.Path}/{image.Name}";
    }
}