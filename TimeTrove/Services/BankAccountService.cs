using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTrove.Client.Models;
using TimeTrove.Data;
using TimeTrove.Data.Models;

namespace TimeTrove.Services;

public interface IBankAccountService
{
    Task<BankAccountDTO> CreateBankAccount(BankAccountDTO bankAccountDto);
    Task<List<BankAccountDTO>?> GetBankAccounts(string? userId);
}


public class BankAccountService : IBankAccountService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    
    public BankAccountService(ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<BankAccountDTO?> CreateBankAccount(BankAccountDTO bankAccountDto)
    {
        BankAccount newAccount = new BankAccount()
        {
            Name = bankAccountDto.Name,
            Balance = bankAccountDto.Balance,
            ApplicationUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User)

        };
        
        Log.Information($"BankAccountToCreateDTO converted to Bank Account successfully.");

        try
        {
            await _context.BankAccounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();
            
            return new BankAccountDTO
            {
                Name = newAccount.Name,
                Balance = newAccount.Balance
            };
        }
        catch (Exception ex)
        {
            Log.Error($"Exception throw when adding Bank Account");
            Log.Debug($"{ex.Message}");
            return null;
        }
    }

    public Task<List<BankAccountDTO>?> GetBankAccounts(string? userId)
    {
        return _context.BankAccounts.Where(b => b.ApplicationUserId == userId)
            .Select(b => new BankAccountDTO
            {
                Name = b.Name, Balance = b.Balance
            })
            .ToListAsync();
    }
}

