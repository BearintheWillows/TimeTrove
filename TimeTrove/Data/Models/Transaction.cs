using System.ComponentModel.DataAnnotations.Schema;
using TimeTrove.Client.Models;
using TimeTrove.Client.Models.Enums.Enums;

namespace TimeTrove.Data.Models;

public class Transaction : AuditableEntity
{
    public int? Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime TransactionDate { get; set; }
    
    public int CalendarDayId { get; set; }
    
    [ForeignKey("CalendarDayId")]
    public CalendarDay CalendarDay { get; set; }
    
    public int PrincipleBankAccountId { get; set; }
    
    [ForeignKey("PrincipleBankAccountId")]
    public BankAccount PrincipleBankAccount { get; set; }
    
    public int SecondaryBankAccountId { get; set; }
    
    [ForeignKey("SecondaryBankAccountId")]
    public BankAccount SecondaryBankAccount { get; set; }

    public static TransactionDTO ToDto(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        return new TransactionDTO
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Type = transaction.Type,
            TransactionDate = transaction.TransactionDate,
            PrincipleBankAccount = BankAccount.ToDto(transaction.PrincipleBankAccount),
            SecondaryBankAccount = BankAccount.ToDto(transaction.SecondaryBankAccount)
        };
    }

    public static Transaction FromDto(TransactionDTO transactionDto)
    {
        ArgumentNullException.ThrowIfNull(transactionDto);
        
        return new Transaction
        {
            Id = transactionDto.Id,
            Amount = transactionDto.Amount,
            Type = transactionDto.Type,
            TransactionDate = transactionDto.TransactionDate,
            PrincipleBankAccountId = transactionDto.PrincipleBankAccount?.Id ?? -1,
            SecondaryBankAccountId = transactionDto.SecondaryBankAccount?.Id ?? -1
        };
    }
    

}