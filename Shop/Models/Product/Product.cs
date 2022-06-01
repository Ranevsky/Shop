namespace Shop.Models;

public sealed class Product
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
            if (!Images[i].IsExist())
            {
                Console.WriteLine($"Remove image id '{Images[i].Id}'");
                Images.Remove(Images[i]);
                isChange = true;
                i--;
            }
        }

        return isChange;
    }
}