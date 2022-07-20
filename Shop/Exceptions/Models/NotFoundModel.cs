namespace Shop.Exceptions.Models;

public class NotFoundModel : ErrorModel
{
    private const string _title = "Not Found";
    private const int _status = StatusCodes.Status404NotFound;
    public NotFoundModel(string? description = null)
        : base(_title, _status, description)
    {

    }
}
