using Microsoft.AspNetCore.Mvc;

namespace Shop.Exceptions.Models;

public class ErrorModel
{
    public string Title { get; init; } = null!;
    public int Status { get; init; }
    public string? Description { get; init; }
    public ActionResult ActionResult { get; private set; } = null!;
    public ErrorModel()
    {

    }
    public ErrorModel(string title, int status, string? description = null)
    {
        Title = title;
        Status = status;
        Description = description;
    }
}
