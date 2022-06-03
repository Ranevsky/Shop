using Microsoft.AspNetCore.Mvc;

using Shop.Models;
using Shop.Models.Repository;

namespace Shop.Controllers;

/// <summary>
/// Not to use, was created as debugging
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[Route("[controller]")]
public sealed class DataBaseController : ControllerBase
{
    private readonly IUnitOfWork uow;
    public DataBaseController(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    /// <summary>
    /// Creates new products
    /// </summary>
    /// <param name="product">Product</param>
    /// <returns>Rerturns ActionResult</returns>
    /// <response code="200">Success</response>
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult> AddProduct(Product product)
    {
        await uow.Products.AddAsync(product);
        await uow.SaveAsync();
        return Ok(new { message = "Product created" });
    }

    /// <summary>
    /// Creates multiple products
    /// </summary>
    /// <param name="products"></param>
    /// <returns>Returns ActionResult</returns>
    /// <response code="200">Success</response>
    [HttpPost("Range")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult> AddRangeProduct(Product[] products)
    {
        await uow.Products.AddRangeAsync(products);
        await uow.SaveAsync();
        return Ok(new { message = "Products created" });
    }

    /// <summary>
    /// Updates product
    /// </summary>
    /// <param name="product">Product</param>
    /// <response code="200">Success</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> ChangeProduct(Product product)
    {
        uow.Products.Update(product);
        await uow.SaveAsync();
        return Ok();
    }
}
