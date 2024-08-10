using TimeTrove.Client.Models.Enums.Enums;
using TimeTrove.Client.Models.Shared;

namespace TimeTrove.Client.Models;

public class BudgetItemDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public Frequency Frequency { get; set; }
    public BankAccountDTO PrimaryBankAccount { get; set; }
    public BankAccountDTO? DestinationBankAccount { get; set; }
}