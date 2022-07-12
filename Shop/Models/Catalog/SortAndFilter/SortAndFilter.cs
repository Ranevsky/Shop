namespace Shop.Models;

public sealed class SortAndFilter
{
    /// <summary>
    /// The type of product that is stored on the database is <b>not case-sensitive</b><br></br>
    /// If <b>Type = null</b>, then filtering won't work<br></br>
    /// Default: <b>null</b>
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// If <b>PriceFilter = null</b>, then filtering won't work<br></br>
    /// Default: <b>null</b>
    /// </summary>
    public PriceFilter? PriceFilter { get; set; }
    /// <summary>
    /// If <b>Sort = null</b>, then sorting won't work<br></br>
    /// Default: <b>null</b>
    /// </summary>
    public SortModel? Sort { get; set; }
    /// <summary>
    /// <b>true</b>  - the products is issued that has a warranty<br></br>
    /// <b>false</b> - the products is issued for which <b>no</b> warranty
    /// <b>null</b>  - filtering won't work<br></br>
    /// Default: <b>null</b>
    /// </summary>
    public bool? Warranty { get; set; }
    /// <summary>
    /// <b>true</b>  - the products that are in stock are issued<br></br>
    /// <b>false</b> - the products that is <b>not</b> in stock is issued
    /// <b>null</b>  - filtering won't work<br></br>
    /// Default: <b>null</b>
    /// </summary>
    public bool? IsStock { get; set; }
}