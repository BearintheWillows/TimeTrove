using Microsoft.AspNetCore.Mvc;
using TimeTrove.Client.Models;
using TimeTrove.Data;
using TimeTrove.Services;

namespace TimeTrove.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private readonly ApplicationDbContext _dbContext;
    
    public BudgetController(IBudgetService budgetService, ApplicationDbContext dbContext)
    {
        _budgetService = budgetService;
        _dbContext = dbContext;
    }
    
    /*[HttpGet]
    public Task<ActionResult<BudgetDTO>> GetBudget()
    {
        
    }*/
}