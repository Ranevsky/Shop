namespace Shop.Models;

public class FilterAndSortModel
{
    public int Count { get; set; } = 1;
    public int Page { get; set; } = 1;
    public string? Type { get; set; }
    public PriceModel? PriceFilter { get; set; }
    public SortModel? Sort { get; set; }
    public bool? Warranty { get; set; }
    public bool? IsStock { get; set; }
}