﻿namespace Shop.Models.Catalog;

public sealed class PriceFilter
{
    /// <summary>
    /// If <b>More = null</b>, then filtering won't work<br></br>
    /// Default: <b>null</b>
    /// </summary>
    public decimal? More { get; set; }
    /// <summary>
    /// If <b>Less = null</b>, then filtering won't work<br></br>
    /// Default: <b>null</b>
    /// </summary>
    public decimal? Less { get; set; }
}