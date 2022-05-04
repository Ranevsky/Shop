using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Shop.Models;

// inheritance from order (field Characteristic)
public class Product
{
    [Key]
    public int Id { get; set; } = default;
    public string Name { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public decimal Price { get; set; } = default;
    public List<string> Images { get; set; } = new();
    public int Popularity { get; set; } = default;
    public string? Description { get; set; } = default;
    public List<string> Characteristic { get; set; } = new();
    public string? Warranty { get; set; } = default;
    /// <summary>
    /// Sets the URL to the image.
    /// </summary>
    /// <returns><b>True</b> - if you need to make changes in the db</returns>
    public bool SetImagesPath()
    {
        bool isChange = false;
        for (int i = 0; i < Images.Count; i++)
        {
            try
            {
                string filePath = $"{Program.PathToImages}/{Images[i]}";
                if (!File.Exists(filePath))
                {
                    throw new Exception($"'{filePath}' not found");
                }

                Images[i] = $"{Program.applicationUrl}{Program.ImageUrl}/{Images[i]}";
                //Images[i].SetPath();
            }
            catch
            {
                Images.Remove(Images[i]);
                isChange = true;
            }
        }
        return isChange;
    }

}

