using Microsoft.AspNetCore.Mvc;
using TimeTrove.Client.Models;
using TimeTrove.Data;
using TimeTrove.Data.Models;
using TimeTrove.Services;

namespace TimeTrove.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    
    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }
    
    [HttpGet("{budgetID}")]
    public async Task<ActionResult<BudgetDTO>> GetBudgetDetails(int budgetID, [FromQuery] bool includeItems = false)
    {
        var budget = await _budgetService.GetBudgetAsync(budgetID, includeItems);

        if (budget == null)
        {
            return NotFound();
        }

        return Ok(budget);
    }

    [HttpPost]
    public async Task<ActionResult<BudgetDTO>> CreateNewBudget(BudgetDTO budgetDto)
    {
        var budget = await _budgetService.CreateBudgetAsync(budgetDto);
        
        /*TODO - Finish Off*/
    }
}