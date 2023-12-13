using eShop.Catalog.API.Domain.Entities;
using eShop.Catalog.API.Persistence;
using eShop.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace eShop.Catalog.API.Controllers;

public class ProductController : BaseController
{
    private readonly ICatalogContext _context;

    public ProductController(ICatalogContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<Product>> List()
    {
        return Ok(await _context.Collection<Product>().Find(_ => true).ToListAsync());
    }
    
    [HttpGet]
    public async Task<ActionResult<Product>> Get(Guid id)
    {
        var product = await _context.Collection<Product>().Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        return product is null ? NotFound() : Ok(product);
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        await _context.Collection<Product>().InsertOneAsync(product);
        return await Get(product.Id);
    }
    
    [HttpPut]
    public async Task<ActionResult<bool>> Update([FromBody] Product product)
    {
        var updateResult = await _context
            .Collection<Product>()
            .ReplaceOneAsync(filter: x => x.Id == product.Id, replacement: product);
        return Ok(updateResult.IsAcknowledged && updateResult.ModifiedCount > 0);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var deleteResult = await _context
            .Collection<Product>()
            .DeleteOneAsync(filter);
        return Ok(deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0);
    }
}