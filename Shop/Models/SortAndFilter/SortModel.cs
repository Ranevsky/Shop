namespace Shop.Models;

public sealed class SortModel
{
    /// <summary>
    /// Default value - <b>true</b><br></br>
    /// <b>Sort_Asc = true</b>, sorting by ascending<br></br>
    /// <b>Sort_Asc = false</b>, sorting by descending
    /// </summary>
    public bool Sort_Asc { get; set; } = true;
    /// <summary>
    /// Sorting by Type, the type is <b>not case-sensitive</b><br></br>
    /// <b>"popularity"</b> - sort by popularity<br></br>
    /// <b>"price"</b> - sort by price
    /// </summary>
    public string Type { get; set; } = null!;
}