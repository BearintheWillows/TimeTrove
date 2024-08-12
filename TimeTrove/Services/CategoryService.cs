using Microsoft.EntityFrameworkCore;
using TimeTrove.Client.Models;
using TimeTrove.Data;
using TimeTrove.Data.Models;

namespace TimeTrove.Services;

public interface ICategoryService
{
    Task<List<CategoryDTO>> GetCategories();
}

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CategoryDTO>> GetCategories()
    {
        return await _dbContext.Categories.Select(c => Category.ToDto(c)).ToListAsync();
    }
}