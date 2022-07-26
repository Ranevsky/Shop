using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Shop.Exceptions;
using Shop.Exceptions.Models;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Controllers;

[Route("[controller]")]
public class ProductTypeController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public ProductTypeController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all types
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet("All")]
    [ProducesResponseType(typeof(IEnumerable<ProductType>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ProductType>> GetAllTypes()
    {
        IEnumerable<ProductType> types = _uow.ProductTypes.GetAll();

        return Ok(types);
    }

    /// <summary>
    /// Delete product type
    /// </summary>
    /// <param name="id">Product type id</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel),StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _uow.ProductTypes.DeleteAsync(id, _uow.Products);

        await _uow.SaveAsync();

        return Ok();
    }

    /// <summary>
    /// Change product type name
    /// </summary>
    /// <param name="id">Product type id</param>
    /// <param name="newName">New name for product</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangeNameAsync(int id, string newName)
    {
        await _uow.ProductTypes.ChangeNameAsync(id, newName);

        await _uow.SaveAsync();

        return Ok();
    }

    /// <summary>
    /// Get product type
    /// </summary>
    /// <param name="id">Product type id</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not found</response>
    [HttpGet]
    [ProducesResponseType(typeof(ProductType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductType>> GetAsync(int id)
    {
        ProductType type = await _uow.ProductTypes.GetAsync(id);

        return type;
    }

    /// <summary>
    /// Add product type
    /// </summary>
    /// <param name="type">Product to add</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddAsync(ProductTypeAddModel type)
    {
        ProductType productType = _mapper.Map<ProductType>(type);
        await _uow.ProductTypes.AddAsync(productType);

        await _uow.SaveAsync();

        return Ok();
    }

    /// <summary>
    /// Get count product type
    /// </summary>
    /// <param name="id">Product type id</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    [HttpGet("Count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> GetCountAsync(int id)
    {
        int count = await _uow.ProductTypes.GetCountAsync(id);

        return Ok(count);
    }

}