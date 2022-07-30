using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Shop.Exceptions.Models;
using Shop.Models.Catalog;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProductController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public ProductController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    /// <summary>
    /// Get product
    /// </summary>
    /// <param name="id">Product id</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Product not found</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductView>> GetProductAsync(int id)
    {
        Product product = await _uow.Products.GetAsync(id, true);
        product.Popularity++;

        ProductView viewProduct = _mapper.Map<ProductView>(product);

        await _uow.SaveAsync();

        return Ok(viewProduct);
    }

    /// <summary>
    /// Gets products after sorting and filtering
    /// </summary>
    /// <param name="sortAndFilter">Sorting and filter model</param>
    /// <param name="paging">Model with pages and quantity of products</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    [HttpGet("Paging")]
    [ProducesResponseType(typeof(CatalogView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CatalogView>> PagingAsync([FromQuery] SortAndFilter sortAndFilter, [FromQuery] PagingModel paging)
    {
        IQueryable<Product> productsQuery = await _uow.Products.SortAndFilterAsync(sortAndFilter, true);

        int count = await productsQuery.CountAsync();

        productsQuery = paging.Paging(productsQuery);

        Product[] products = productsQuery.ToArray();

        CatalogView catalog = _mapper.Map<CatalogView>(products);
        catalog.Count = count;

        if (catalog.IsNeedSave)
        {
            await _uow.SaveAsync();
        }

        return Ok(catalog);
    }

    /// <summary>
    /// Add product
    /// </summary>
    /// <param name="productModel">Model for Product</param>
    /// <response code="201">Success</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProductView), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddProductAsync(ProductAddModel productModel)
    {
        if (!ModelState.IsValid)
        {
            return new BadRequestModel("Model is not valid").ActionResult;
        }

        Product product = _mapper.Map<Product>(productModel);
        await _uow.Products.AddAsync(product, _uow.ProductTypes);

        await _uow.SaveAsync();
#warning change actionResult
        ProductView productView = _mapper.Map<ProductView>(product);
        return base.CreatedAtAction("GetProduct", new { id = product.Id }, productView);
    }

    /// <summary>
    /// Delete Product
    /// </summary>
    /// <param name="id">Product id</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProductAsync(int id)
    {
        await _uow.Products.DeleteAsync(id);

        await _uow.SaveAsync();

        return Ok();
    }

    /// <summary>
    /// Add images for Product
    /// </summary>
    /// <param name="id">Product id</param>
    /// <param name="files">Image files</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    [HttpPost("AddImages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddImagesAsync(int id, IFormFileCollection files)
    {
        await _uow.Products.AddImagesAsync(id, files);

        await _uow.SaveAsync();

        return Ok();
    }

    /// <summary>
    /// Delete images from Product
    /// </summary>
    /// <param name="id">Product id</param>
    /// <param name="imagesId">Collection </param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    [HttpDelete("DeleteImage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteImagesAsync(int id, IEnumerable<int> imagesId)
    {
        await _uow.Products.DeleteImagesAsync(id, imagesId);

        await _uow.SaveAsync();

        return Ok();
    }

    /// <summary>
    /// Set warranty for product
    /// </summary>
    /// <param name="productId">Product id</param>
    /// <param name="warrantyId">Warranty id<br></br>If warranty id is empty then <b>warranty = null</b></param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    [HttpPut("SetWarranty")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> SetWarrantyAsync(int productId, int? warrantyId)
    {
        if (warrantyId == null)
        {
            await _uow.Products.SetWarrantyAsync(productId, null);
        }
        else
        {
            await _uow.Products.SetWarrantyAsync(productId, (int)warrantyId, _uow.Warranties);
        }

        await _uow.SaveAsync();

        return Ok();
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="id">Product id</param>
    /// <param name="model">Model for Product</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Product not found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task UpdateProductAsync(int id, ProductUpdateModel model)
    {
        await _uow.Products.UpdateAsync(id, model);

        await _uow.SaveAsync();
    }
}