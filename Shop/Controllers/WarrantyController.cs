using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Shop.Exceptions.Models;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Controllers;

[Route("[Controller]")]
public class WarrantyController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public WarrantyController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    /// <summary>
    /// Get warranty
    /// </summary>
    /// <param name="id">Warranty id</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Product not found</response>
    [HttpGet]
    [ProducesResponseType(typeof(Warranty), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<Warranty> GetAsync(int id)
    {
        Warranty warranty = await _uow.Warranties.GetAsync(id);

        return warranty;
    }

    /// <summary>
    /// Get all warranties
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet("All")]
    [ProducesResponseType(typeof(IEnumerable<Warranty>), StatusCodes.Status200OK)]
    public IEnumerable<Warranty> GetAllAsync()
    {
        IEnumerable<Warranty> warranties = _uow.Warranties.GetAll();

        return warranties;
    }

    /// <summary>
    /// Get count contains products in warranty
    /// </summary>
    /// <param name="id">Warranty id</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Product not found</response>
    [HttpGet("Count")]
    [ProducesResponseType(typeof(WarrantyCountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<WarrantyCountModel> GetCountAsync(int id)
    {
        WarrantyCountModel countModel = await _uow.Warranties.GetCountAsync(id);

        return countModel;
    }

    /// <summary>
    /// Add warranty
    /// </summary>
    /// <param name="warrantyModel">Model for warranty</param>
    /// <response code="200">Success</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task AddAsync(WarrantyAddModel warrantyModel)
    {
        Warranty warranty = _mapper.Map<Warranty>(warrantyModel);
        await _uow.Warranties.AddAsync(warranty);

        await _uow.SaveAsync();
    }

    /// <summary>
    /// Update warranty
    /// </summary>
    /// <param name="id">Warranty id</param>
    /// <param name="model">Model for warranty</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Product not found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task UpdateAsync(int id, [FromBody] WarrantyUpdateModel model)
    {
        await _uow.Warranties.UpdateAsync(id, model);

        await _uow.SaveAsync();
    }

    /// <summary>
    /// Delete warranty
    /// </summary>
    /// <param name="id">Warranty id</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Product not found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task DeleteAsync(int id)
    {
        await _uow.Warranties.DeleteAsync(id);

        await _uow.SaveAsync();
    }

}
