using System.Text.Json.Serialization;

using Shop.Constants;

namespace Shop.Models.Product;

public class ProductImage : Image
{
    protected override string FullPath => $"{PathConst.ImageFullPath}{PathConst.ProductPath}/{Path}/{Name}";
    public override string Url => $"{UrlConst.FullImageUrl}{PathConst.ProductPath}/{Path}/{Name}";

    [JsonIgnore]
    public Product? Product { get; set; }
}
