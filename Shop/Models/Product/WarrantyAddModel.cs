using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Product;

public class WarrantyAddModel
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
