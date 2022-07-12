using AutoMapper;

using Shop.Models.View;

namespace Shop.Models;

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
            .ForMember(p => p.Warranty, opt =>
            {
                opt.MapFrom(p => p.Warranty == null ? null : p.Warranty.Description);
            })
            .ForMember(p => p.Description, opt =>
            {
                opt.MapFrom(p => p.Description == null ? null : p.Description.Text);
            })
            .ForMember(p => p.Characteristic, opt =>
            {
                opt.MapFrom(p => p.Characteristics);
            });

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

        CreateMap<Image, string>()
            .ConvertUsing(i => i.GetUrl());

        CreateMap<Image, ImageView>()
            .ForMember(i => i.Url, opt =>
            {
                opt.MapFrom(i => i.GetUrl());
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
    }
}