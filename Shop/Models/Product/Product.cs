using Shop.Constants;

namespace Shop.Models.Product;

public sealed class Product : IDelete
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ProductType Type { get; set; } = null!;
    public decimal Price { get; set; }
    public List<Image> Images { get; set; } = new();
    public int Popularity { get; set; }
    public Description? Description { get; set; }
    public List<Characteristic>? Characteristics { get; set; } = new();
    public Warranty? Warranty { get; set; }
    public bool IsStock { get; set; }
    public bool IsNeedDeleteImage()
    {
        bool isChange = false;

        for (int i = 0; i < Images.Count; i++)
        {
            if (!Images[i].IsExists)
            {
                Console.WriteLine($"Remove image id '{Images[i].Id}'");
#warning Image removing disabled
                //Images.Remove(Images[i]);
                isChange = true;
                //i--;
            }
        }

        return isChange;
    }
    public void DeleteImage(Image image)
    {
        Image? img = Images.Find(i => object.ReferenceEquals(i, image));
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