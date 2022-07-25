
using Shop.Exceptions.Models;

namespace Shop.Exceptions;

public class NotFoundException : ActionResultException
{
    public NotFoundException(string? description = null) : base(description)
    {
        ErrorModel = new NotFoundModel(description);
        ActionResult = ErrorModel.ActionResult;
    }
}