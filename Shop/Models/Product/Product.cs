using Shop.Constants;

namespace Shop.Models.Product;

public sealed class Product : IDelete
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ProductType Type { get; set; } = null!;
    public decimal Price { get; set; }
    public List<ProductImage> Images { get; set; } = new();
    public int Popularity { get; set; }
    public Description? Description { get; set; }
    public List<Characteristic>? Characteristics { get; set; } = new();
    public Warranty? Warranty { get; set; }
    public bool IsStock { get; set; }
    public bool IsNeedDeleteImage()
    {
        int count = Images.RemoveAll(i => !i.IsExists);
#warning Add loger in deleting image

        bool isNeedSave = count > 0;
        if (isNeedSave)
        {
            Console.WriteLine($"Delete {count} image in product with id = {Id}");
        }

        return isNeedSave;
    }
    public void DeleteImage(ProductImage image)
    {
        ProductImage? img = Images.Find(i => object.ReferenceEquals(i, image));
        if (img == null)
        {
            return;
        }

        Images.Remove(img);
        img.Delete();
    }

    private string FullPath => $"{PathConst.ImageFullPath}{PathConst.ProductPath}/{Id}";
    public bool IsExists => Directory.Exists(FullPath);
    public void Delete()
    {
        if (IsExists)
        {
            Directory.Delete(FullPath, true);
        }

#warning Added logger information
        Images.Clear();
        Console.WriteLine($"Delete product id: '{Id}'");
    }
}