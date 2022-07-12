using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Shop.Models;
using Shop.Models.Repository;
using Shop.Models.View;

namespace Shop.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProductController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public ProductController(IUnitOfWork uow, IMapper mapper)
    {
        this._uow = uow;
        this._mapper = mapper;
    }

    /// <summary>
    /// Get product
    /// </summary>
    /// <param name="id">Product id</param>
    /// <returns>Returns ProductView</returns>
    /// <response code="200">Success</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductView>> GetProductViewAsync(int id)
    {
        Product? product = await _uow.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        product.Popularity++;
        await _uow.SaveAsync();

        ProductView? viewProduct = _mapper.Map<ProductView>(product);
        return Ok(viewProduct);
    }

    /// <summary>
    /// Gets products after sorting and filtering
    /// </summary>
    /// <param name="sortAndFilter">Sorting and filter model</param>
    /// <param name="paging">Model with pages and quantity of products</param>
    /// <returns>Returns CatalogView</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    [HttpGet("Paging")]
    [ProducesResponseType(typeof(CatalogView),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public ActionResult<CatalogView> Paging([FromQuery] SortAndFilter sortAndFilter, [FromQuery] PagingModel paging)
    {
        IQueryable<Product> productsQuery;
        try
        {
            productsQuery = _uow.Products.Paging(sortAndFilter).Result;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        CatalogView? catalog = new() { CountProducts = productsQuery.LongCount() };

        productsQuery = Paging(productsQuery, paging);

        Product[] products = productsQuery.ToArray();
        ProductInCatalogView[] productCatalog = _mapper.Map<ProductInCatalogView[]>(products);
        catalog.Products = productCatalog;

        return Ok(catalog);
    }

    [HttpPut]
    public async Task<ActionResult> AddProductAsync(ProductAddModel productModel)
    {
        if (!ModelState.IsValid)
        {
            return Content("Model is not valid");
        }

        Product product = _mapper.Map<Product>(productModel);

        await _uow.Products.AddAsync(product);
        await _uow.SaveAsync();

        ProductView productView = _mapper.Map<ProductView>(product);
        return base.CreatedAtAction("GetProductView", new { id = product.Id }, productView);
    }

    [NonAction]
    private static IQueryable<T> Paging<T>(IQueryable<T> query, PagingModel paging)
    {
        return query.Skip((paging.Page - 1) * paging.Count).Take(paging.Count);
    }

}