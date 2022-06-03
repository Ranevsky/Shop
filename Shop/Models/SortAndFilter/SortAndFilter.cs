namespace Shop.Models;

public sealed class SortAndFilter
{
    /// <summary>
    /// The count you need to withdraw<br></br>
    /// If <b>Count &lt; 0</b>, then outputs <b>all</b> products
    /// </summary>
    public int Count { get; set; } = 1;
    /// <summary>
    /// Which page to display
    /// </summary>
    public int Page { get; set; } = 1;
    /// <summary>
    /// The type of product that is stored on the database is <b>not case-sensitive</b><br></br>
    /// If <b>Type = null</b>, then filtering won't work 
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// If <b>PriceFilter = null</b>, then filtering won't work 
    /// </summary>
    public PriceFilter? PriceFilter { get; set; }
    /// <summary>
    /// If <b>Sort = null</b>, then sorting won't work 
    /// </summary>
    public SortModel? Sort { get; set; }
    /// <summary>
    /// <b>null</b>  - filtering won't work<br></br>
    /// <b>true</b>  - the products is issued that has a warranty<br></br>
    /// <b>false</b> - the products is issued for which <b>no</b> warranty
    /// </summary>
    public bool? Warranty { get; set; }
    /// <summary>
    /// <b>null</b>  - filtering won't work<br></br>
    /// <b>true</b>  - the products that are in stock are issued<br></br>
    /// <b>false</b> - the products that is <b>not</b> in stock is issued
    /// </summary>
    public bool? IsStock { get; set; }
}