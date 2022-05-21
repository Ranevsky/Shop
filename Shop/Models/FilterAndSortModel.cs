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

public class SortModel
{
    public bool Sort_Asc { get; set; } = true;
    public string Type { get; set; } = null!;
}

public class PriceModel
{
    public decimal? More { get; set; } // 100 
    public decimal? Less { get; set; } // 200
} // products.Price > model.Price.More
  // and
  // products.Price < model.Price.Less

  // >100 & <200