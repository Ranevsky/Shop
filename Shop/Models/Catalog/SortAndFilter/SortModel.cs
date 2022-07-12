namespace Shop.Models;

public sealed class SortModel
{
    /// <summary>
    /// <b>SortAsc = true</b>, sorting by ascending<br></br>
    /// <b>SortAsc = false</b>, sorting by descending<br></br>
    /// Default: <b>true</b>
    /// </summary>
    public bool SortAsc { get; set; } = true;
    /// <summary>
    /// <b>0</b> - Popularity<br></br>
    /// <b>1</b> - Price<br></br>
    /// Default: <b>0</b>
    /// </summary>
    public TypeSort Type { get; set; } = 0;

    public enum TypeSort : byte
    {
        Popularity = 0,
        Price = 1
    }
}