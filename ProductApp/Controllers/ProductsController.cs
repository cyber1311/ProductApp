using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ProductApp.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{

	private readonly ProductsContext _context;

	public ProductsController(ProductsContext context)
	{
		_context = context;
	}

	[HttpPost("create")]
	public async Task<IActionResult> Create(Product product)
	{
		if (await _context.Products.FindAsync(product.Id) != null) return BadRequest("Товар с таким Id уже существует");
		if (product.Price != null && product.Price <= 0) return BadRequest("Цена товара должна быть положительным числом");
		if (await _context.ProductCategories.FindAsync(product.CategoryId) == null) return BadRequest("Указан несуществующий CategoryId");
		await _context.Products.AddAsync(product);
		await _context.SaveChangesAsync();
		if (await _context.Products.FindAsync(product.Id) != null) return Created();
		return StatusCode(500, "Не удалось добавить товар");
	}

	[HttpPut("update/{id}")]
	public async Task<IActionResult> Update(int id, Product updatedProduct)
	{
		if (id != updatedProduct.Id) return BadRequest();
		Product? product = await _context.Products.FindAsync(id);
		if (product == null) return NotFound("Товара с таким Id не существует");
		if(updatedProduct.Name != null) product.Name = updatedProduct.Name;
		if (updatedProduct.Price != null){
			if(updatedProduct.Price <= 0) return BadRequest("Цена товара должна быть положительным числом");
			product.Price = updatedProduct.Price;
		}
		if (updatedProduct.Description != null) product.Description = updatedProduct.Description;
		if (updatedProduct.CategoryId != null){
			if (await _context.ProductCategories.FindAsync(updatedProduct.CategoryId) == null) return BadRequest("Указан несуществующий CategoryId");
			product.CategoryId = updatedProduct.CategoryId;
		}
		_context.Products.Update(product);
		await _context.SaveChangesAsync();
		if (await _context.Products.FindAsync(product.Id) != null) return Ok();
		return StatusCode(500, "Не удалось обновить товар");
	}

	[HttpGet("get_all")]
	public async Task<ActionResult<IEnumerable<Product>>> GetAll()
	{
		return await _context.Products.ToListAsync();
	}

	[HttpGet("get/{id}")]
	public async Task<ActionResult<Product>> Get(int id)
	{
		Product? product = await _context.Products.FindAsync(id);
		if(product != null) return Ok(product);
		return NotFound("Товара с таким Id не существует");
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		Product? product = await _context.Products.FindAsync(id);
		if (product == null) return NotFound("Товара с таким Id не существует");
		_context.Products.Remove(product);
		await _context.SaveChangesAsync();
		var deletedProduct = await _context.Products.FindAsync(product.Id);
		if (deletedProduct == null) return NoContent();
		return StatusCode(500, "Не удалось удалить товар");
	}


}
