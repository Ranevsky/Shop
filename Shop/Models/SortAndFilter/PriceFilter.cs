namespace Shop.Models;

public sealed class PriceFilter
{
    /// <summary>
    /// If <b>More = null</b>, then filtering won't work 
    /// </summary>
    public decimal? More { get; set; }
    /// <summary>
    /// If <b>Less = null</b>, then filtering won't work 
    /// </summary>
    public decimal? Less { get; set; }
}