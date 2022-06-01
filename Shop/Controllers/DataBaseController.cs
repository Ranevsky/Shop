using Microsoft.AspNetCore.Mvc;

using Shop.Models;
using Shop.Models.Repository;

namespace Shop.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class DataBaseController : ControllerBase
{
    private readonly IUnitOfWork uow;
    public DataBaseController(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct(Product product)
    {
        await uow.Products.AddAsync(product);
        await uow.SaveAsync();
        return Ok(new { message = "Product created" });
    }

    [HttpPost("Range")]
    public async Task<ActionResult> AddRangeProduct(Product[] products)
    {
        await uow.Products.AddRangeAsync(products);
        await uow.SaveAsync();
        return Ok(new { message = "Products created" });
    }

    [HttpPut]
    public async Task<ActionResult<Product>> ChangeProduct(Product product)
    {
        uow.Products.Update(product);
        await uow.SaveAsync();
        return product;
    }
}
