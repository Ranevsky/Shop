using AutoMapper;

using Shop.Models.Catalog;
using Shop.Models.Product;

namespace Shop.Profiles;

public sealed class ProductProfile : Profile
{
    public ProductProfile()
    {
        // ProductView
        CreateMap<Product, ProductView>()
            .BeforeMap((product, productView) =>
             {
                 bool isNeedSave = IsNeedDeleteImage(product);
                 productView.IsNeedSave = isNeedSave;
             })
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

        CreateMap<Warranty, ProductWarrantyView>();

        CreateMap<Characteristic, string>()
            .ConvertUsing(p => p.Description);

        // ProductInCatalogView
        CreateMap<Product, ProductInCatalogView>()
            .ForMember(productView => productView.ProductType, opt =>
             {
                 opt.MapFrom(product => product.Type.Name);
             })
            .ForMember(productView => productView.Image, opt =>
             {
                 opt.MapFrom(product => product.Images.FirstOrDefault());
             });

        CreateMap<Product[], CatalogView>()
            .BeforeMap((products, catalog) =>
            {
                bool isNeedSave = IsNeedDeleteImage(products);
                catalog.IsNeedSave = isNeedSave;
            })
            .ForMember(catalog => catalog.Products, opt =>
            {
                opt.MapFrom(products => products);
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

    public bool IsNeedDeleteImage(params Product[] products)
    {
        bool isNeedSave = false;
        foreach (Product? product in products)
        {
            if (product?.IsNeedDeleteImage() ?? false)
            {
                isNeedSave = true;
            }
        }
        return isNeedSave;
    }
}