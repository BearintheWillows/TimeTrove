using Microsoft.Identity.Client;
using TimeTrove.Client.Models;
using TimeTrove.Client.Models.Enums.Enums;
using TimeTrove.Client.Models.Shared;

namespace TimeTrove.Data.Models;


    public class BudgetItem : AuditableEntity
    {
        public int Id { get; set; }
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

        public static BudgetItemDTO ToDto(BudgetItem budgetItem)
        {
        ArgumentNullException.ThrowIfNull(budgetItem);

        var budgetItemDto = new BudgetItemDTO
            {
                Id = budgetItem.BudgetID,
                Amount = budgetItem.Amount,
                Type = budgetItem.Type,
                Frequency = budgetItem.Frequency,
                PrimaryBankAccount = BankAccount.ToDto(budgetItem.PrimaryBankAccount),
                DestinationBankAccount = budgetItem.DestinationBankAccount != null 
                    ? BankAccount.ToDto(budgetItem.DestinationBankAccount)
                    : null
            };

            return budgetItemDto;
        }
    }