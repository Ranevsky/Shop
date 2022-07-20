using Microsoft.AspNetCore.Mvc;

using Shop.Exceptions.Models;

namespace Shop.Exceptions;

public class BadRequestException : ActionResultException
{
    public BadRequestException(string? description = null) : base(description)
    {
        ErrorModel = new BadRequestModel(description);
        ActionResult = new ObjectResult(ErrorModel) { StatusCode = ErrorModel.Status };
    }
}
