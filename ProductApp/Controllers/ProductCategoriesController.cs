using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ProductApp.Controllers;

[ApiController]
[Route("product_categories")]
public class ProductCategoriesController : ControllerBase
{

	private readonly ProductsContext _context;

	public ProductCategoriesController(ProductsContext context)
	{
		_context = context;
	}

	[HttpPost("create")]
	public async Task<IActionResult> Create(ProductCategory category)
	{
		if(await _context.ProductCategories.FindAsync(category.Id) != null) return BadRequest("Категория с таким Id уже существует");
		await _context.ProductCategories.AddAsync(category);
		await _context.SaveChangesAsync();
		if (await _context.ProductCategories.FindAsync(category.Id) != null) return Created();
		return StatusCode(500, "Не удалось добавить категорию");
	}

	[HttpPut("update/{id}")]
	public async Task<IActionResult> Update(int id, ProductCategory updatedCategory)
	{
		ProductCategory? category = await _context.ProductCategories.FindAsync(id);
		if (category == null) return NotFound("Категории с таким Id не существует");
		if (updatedCategory.Name != null) category.Name = updatedCategory.Name;
		if (updatedCategory.Description != null) category.Description = updatedCategory.Description;

		_context.ProductCategories.Update(category);
		await _context.SaveChangesAsync();
		if (await _context.ProductCategories.FindAsync(category.Id) != null) return Ok();
		return StatusCode(500, "Не удалось обновить категорию");
	}

	[HttpGet("get_all")]
	public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAll()
	{
		return await _context.ProductCategories.ToListAsync();
	}

	[HttpGet("get/{id}")]
	public async Task<ActionResult<ProductCategory>> GetById(int id)
	{
		ProductCategory? category = await _context.ProductCategories.FindAsync(id);
		if (category != null) return Ok(category);
		return NotFound("Категории с таким Id не существует");
	}

	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		ProductCategory? category = await _context.ProductCategories.FindAsync(id);
		if (category == null) return NotFound("Категории с таким Id не существует");
		_context.ProductCategories.Remove(category);
		await _context.SaveChangesAsync();
		if (await _context.ProductCategories.FindAsync(category.Id) == null) return NoContent();
		return StatusCode(500, "Не удалось удалить категорию");
	}


}
