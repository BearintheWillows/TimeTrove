using Microsoft.Identity.Client;
using TimeTrove.Data.Models.Enums;

namespace TimeTrove.Data.Models;


    public class BudgetItem : AuditableEntity
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public Frequency Frequency { get; set; }
        public int BudgetID { get; set; }
        public Budget Budget { get; set; }

        // Foreign key for PrimaryBankAccount
        public int PrimaryBankAccountId { get; set; }

        // Navigation property for PrimaryBankAccount
        public BankAccount PrimaryBankAccount { get; set; }

        // Foreign key for DestinationBankAccount
        // This is nullable because the destination bank account is optional
        public int? DestinationBankAccountId { get; set; }
    
        // Navigation property for DestinationBankAccount
        // This is also nullable
        public BankAccount? DestinationBankAccount { get; set; }
    }
    


public class Frequency
{
    public int Count { get; set; }
    public TimeUnit Unit { get; set; }
}