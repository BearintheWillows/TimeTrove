using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Get()
    {
        var categories = _categoryService.GetCategories();
        
        return Ok(categories);
    }
}