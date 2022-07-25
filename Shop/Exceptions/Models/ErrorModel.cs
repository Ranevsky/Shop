using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;

namespace Shop.Exceptions.Models;

public class ErrorModel
{
    public string Title { get; init; } = null!;
    public int Status { get; init; }
    public string? Description { get; init; }
    [JsonIgnore]
    public ActionResult ActionResult { get; private set; } = null!;
    public ErrorModel(string title, int status, string? description = null)
    {
        Title = title;
        Status = status;
        Description = description;

        ActionResult = new ObjectResult(this) { StatusCode = status };
    }
}
