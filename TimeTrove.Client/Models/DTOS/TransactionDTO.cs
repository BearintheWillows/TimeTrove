using System.ComponentModel.DataAnnotations.Schema;
using TimeTrove.Client.Models.Enums.Enums;

namespace TimeTrove.Client.Models;

public class TransactionDTO
{
    public int? Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime TransactionDate { get; set; }
    public BankAccountDTO PrincipleBankAccount { get; set; }
    public BankAccountDTO SecondaryBankAccount { get; set; }
}