using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Shop.Models;
using Shop.Database;

namespace Shop.Controllers;

[ApiController]
[Route("Product")]
public class ProductController : ControllerBase
{
    private readonly ApplicationContext _db = null!;
    public ProductController(ApplicationContext db)
    {
        _db = db;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound(new {message = "Not found"});
        }
        product.Popularity++;
        await _db.SaveChangesAsync();
        await SetPathToImageAsync(product);
        return Ok(product);
    }

    [HttpGet]
    [Route("Count/{count:int}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetCountAsync(int count, int begin = 1)
    {
        var products = _db.Products.Skip((begin-1)*count).Take(count).ToArray();
        if (products.Length == 0)
        {
            return NotFound(new { message = "Products are missing" });
        }

        await SetPathToImageAsync(products);
        return Ok(products);
    }

    [HttpGet]
    [Route("All")]
    public ActionResult GetAll()
    {
        return RedirectToAction("GetCount", "Product", new {count = -1});
    }

    [HttpGet]
    [Route("Popularity")]
    public async Task<ActionResult<IEnumerable<Product>>> GetPopularity(int count, int begin = 1)
    {
        var products = _db.Products.Skip((begin - 1) * count).Take(count).OrderByDescending(p => p.Popularity).Take(count).ToArray();
        if (products.Length == 0)
        {
            return NotFound(new { message = "Products are missing" });
        }


        await SetPathToImageAsync(products);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> AddProductAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return Ok(new { message = "Product created" });
    }

    [HttpPut]
    public async Task<ActionResult<Product>> ChangeProduct(int id, Product product)
    {
        var productBefore = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (productBefore == null)
        {
            return NotFound(new { message = "Not found product" });
        }
        _db.Entry(productBefore).CurrentValues.SetValues(product);
        await _db.SaveChangesAsync();
        return product;
    }

    [NonAction]
    public async Task SetPathToImageAsync(params Product[] products)
    {
        bool isChange = false;
        foreach (var product in products)
        {
            if (!isChange)
            {
                isChange = product.SetImagesPath();
                continue;
            }
            product.SetImagesPath();
        }
        
        if (isChange)
        {
            await _db.SaveChangesAsync();
        }
    }
}