namespace Shop.Models.Catalog;

public class PagingModel
{
    /// <summary>
    /// The count you need to withdraw<br></br>
    /// If <b>Count &lt; 0</b>, then outputs <b>all</b> products<br></br>
    /// Default: <b>1</b>
    /// </summary>
    public int Count { get; set; } = 1;
    /// <summary>
    /// Which page to display<br></br>
    /// Default: <b>1</b>
    /// </summary>
    public int Page { get; set; } = 1;
}
