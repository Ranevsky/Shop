using Microsoft.AspNetCore.Mvc;

using Shop.Exceptions.Models;

namespace Shop.Exceptions;

public abstract class ActionResultException : Exception
{
    public ErrorModel ErrorModel { get; protected set; } = null!;
    public ActionResult ActionResult { get; protected set; } = null!;
    protected ActionResultException(string? message) : base(message)
    {

    }
}
