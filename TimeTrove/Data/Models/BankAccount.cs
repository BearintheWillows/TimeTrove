using System.ComponentModel.DataAnnotations;
using TimeTrove.Client.Models;

namespace TimeTrove.Data.Models;

public class BankAccount : AuditableEntity
{
    
    //Properties
    
    public int? Id { get; set; }
    
    [Required(ErrorMessage = "Bank account name is required.")]
    [MaxLength(100, ErrorMessage = "Bank account name cannot exceed 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Bank account Balance is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Balance must be a non-negative value.")]
    [DataType(DataType.Currency)]
    public decimal Balance { get; set; }
    
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    
    
    // Methods
    
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be greater than zero.");
        }

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be greater than zero.");
        }

        if (Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds for withdrawal.");
        }

        Balance -= amount;
    }

    public static BankAccountDTO ToDto(BankAccount bankAccount)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);

        return new BankAccountDTO
        {
            Id = bankAccount.Id ?? -1,
            Name = bankAccount.Name,
            Balance = bankAccount.Balance
        };
    }
}