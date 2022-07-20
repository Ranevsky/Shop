namespace Shop.Exceptions.Models;

public class BadRequestModel : ErrorModel
{
    private const string _title = "Bad Request";
    private const int _status = StatusCodes.Status400BadRequest;
    public BadRequestModel(string? description = null)
        : base(_title, _status, description)
    {

    }
}
