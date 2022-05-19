using Shop.Models;
using Shop.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers;

[ApiController]
[Route("[controller]")]
public class DataBaseController : ControllerBase
{
    private readonly IUnitOfWork uow;
    public DataBaseController(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    [HttpPost]
    public ActionResult AddProduct(Product product)
    {
        uow.Products.Add(product);
        uow.Save();
        return Ok(new { message = "Product created" });
    }

    [HttpPost("Range")]
    public ActionResult AddRangeProduct(Product[] products)
    {
        uow.Products.AddRange(products);
        uow.Save();
        return Ok(new { message = "Products created" });
    }
    
    [HttpPut]
    public ActionResult<Product> ChangeProduct(Product product)
    {
        uow.Products.Update(product);
        uow.Save();
        return product;
    }

}
