using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Shop.Models;
using Shop.Models.Repository;
using Shop.Models.View;

namespace Shop.Controllers;

[ApiController]
[Route("[controller]")]
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
        Product? product = uow.Products.Find(id);
        if (product == null)
        {
            return NotFound(new { message = "Not found" });
        }
        product.Popularity++;
        uow.Save();

        ProductView? viewProduct = mapper.Map<ProductView>(product);
        return Ok(viewProduct);
    }

    [HttpGet("Paging")]
    public ActionResult<CatalogView> Paging([FromQuery] SortAndFilter model)
    {
        IQueryable<Product> productsQuery;
        try
        {
            productsQuery = uow.Products.Paging(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        CatalogView? catalog = new() { CountProudcts = productsQuery.LongCount() };

        productsQuery = productsQuery.Skip((model.Page - 1) * model.Count).Take(model.Count);
        ProductInCatalogView[]? productCatalog = mapper.Map<ProductInCatalogView[]>(productsQuery.ToArray());

        catalog.Products = productCatalog;

        return Ok(catalog);
    }
}