using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TimeTrove.Client.Models;
using TimeTrove.Services;

namespace TimeTrove.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BankAccountController : ControllerBase
{
    private readonly IBankAccountService _bankAccountService;

    public BankAccountController(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    [HttpPost]
    public async Task<ActionResult<BankAccountDTO>> CreateBankAccount(BankAccountDTO bankAccountDto)
    {
        var result = await _bankAccountService.CreateBankAccount(bankAccountDto);

        if (result == null)
        {
            Log.Debug($"Failed to Create Bank Account for user {User.FindFirstValue(ClaimTypes.Email)}");
            return BadRequest("Failed to create bank account");
        }

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BankAccountDTO>>> GetBankAccounts()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var bankAccounts = await _bankAccountService.GetBankAccounts(userId);
        return Ok(bankAccounts);
    }
    
}