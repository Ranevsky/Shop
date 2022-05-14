using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Shop.Models;
using Shop.Models.View;
using Shop.Models.Repository;

namespace Shop.Controllers;

[ApiController]
[Route("Product")]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;
    public ProductController(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public ActionResult<ProductView> Get(int id)
    {
        var product = uow.Products.Find(id);
        if (product == null)
        {
            return NotFound(new {message = "Not found"});
        }
        product.Popularity++;
        uow.Save();

        var viewProducts = mapper.Map<ProductView>(product);
        return Ok(viewProducts);
    }

    [HttpGet("Count/{count:int}")]
    public ActionResult<IEnumerable<ProductView>> GetCount(int count, int begin = 1)
    {
        var products = uow.Products.Paging((begin - 1) * count, count).ToArray();
        if (products.Length == 0)
        {
            return NotFound(new { message = "Products are missing" });
        }

        var viewProducts = mapper.Map<ProductView[]>(products);
        return Ok(viewProducts);
    }

    [HttpGet("All")]
    public ActionResult GetAll()
    {
        return RedirectToAction("GetCount", "Product", new {count = -1});
    }

    [HttpGet("Popularity")]
    public ActionResult<IEnumerable<ProductView>> GetPopularity(int count, int begin = 1)
    {
        var products = uow.Products.Paging((begin - 1) * count, count, p => p.Popularity, false).ToArray();
        if (products.Length == 0)
        {
            return NotFound(new { message = "Products are missing" });
        }

        var viewProducts = mapper.Map<ProductView[]>(products);
        return Ok(viewProducts);
    }

    [HttpPost]
    public ActionResult AddProduct(Product product)
    {
        uow.Products.Add(product);
        uow.Save();
        return Ok(new { message = "Product created" });
    }

    [HttpPut]
    public ActionResult<Product> ChangeProduct(Product product)
    {
        uow.Products.Update(product);
        uow.Save();
        return product;
    }
}