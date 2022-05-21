﻿using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Shop.Models;
using Shop.Models.View;
using Shop.Models.Repository;

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


    [HttpGet("{id:int}")] // Поменять ProductView
    public ActionResult<ProductView> Get(int id)
    {
        var product = uow.Products.Find(id);
        if (product == null)
        {
            return NotFound(new {message = "Not found"});
        }
        product.Popularity++;
        uow.Save();

        var viewProduct = mapper.Map<ProductView>(product);
        return Ok(viewProduct);
    }
    
    [HttpGet("Count")]
    public ActionResult<int> GetCount()
    {
        int count = uow.Products.Count();
        return Ok(count);
    }

    [HttpGet("Paging")]
    public ActionResult<Catalog> Paging([FromQuery] FilterAndSortModel model)
    {
        IQueryable<Product> productsQuery = null!;
        try
        {
            productsQuery = uow.Products.Page(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var catalog = new Catalog();
        catalog.CountProudcts = productsQuery.LongCount();

        productsQuery = productsQuery.Skip((model.Page - 1) * model.Count).Take(model.Count);
        var productCatalog = mapper.Map<ProductCatalogView[]>(productsQuery.ToArray());

        catalog.Products = productCatalog;

        return Ok(catalog);
    }

    [HttpGet("All")]
    public ActionResult GetAll()
    {
        return RedirectToAction("Page", "Product", new {count = -1, page = 1});
    }

    [HttpGet("Page")]
    public ActionResult<IEnumerable<ProductCatalogView>> Page(int count, int page = 1)
    {
        var products = uow.Products.Paging((page - 1) * count, count).ToArray();
        if (products.Length == 0)
        {
            return NotFound(new { message = "Products are missing" });
        }

        var viewProducts = mapper.Map<ProductCatalogView[]>(products);
        return Ok(viewProducts);
    }

    [HttpGet("Popularity")]
    public ActionResult<IEnumerable<ProductCatalogView>> PopularityPage(int count, int page = 1)
    {
        var products = uow.Products.Paging((page - 1) * count, count, p => p.Popularity, false).ToArray();
        if (products.Length == 0)
        {
            return NotFound(new { message = "Products are missing" });
        }

        var viewProducts = mapper.Map<ProductCatalogView[]>(products);
        return Ok(viewProducts);
    }
}