namespace Shop.Models;

public interface IDelete
{
    public bool IsExists { get; }
    public void Delete();
}
