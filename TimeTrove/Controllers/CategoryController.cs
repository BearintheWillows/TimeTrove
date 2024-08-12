using Microsoft.AspNetCore.Mvc;
using TimeTrove.Client.Models;
using TimeTrove.Services;

namespace TimeTrove.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
       List<CategoryDTO> categories = await _categoryService.GetCategories();
        
        return Ok(categories);
    }
}