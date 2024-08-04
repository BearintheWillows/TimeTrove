using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TimeTrove.Client.Models;

public class BankAccountDTO
{
    [Required]
    [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "Name must be betwen 1-250 Charecters.")]
    public required string Name { get; set; }

    [RegularExpression(@"^-?\d+(\.\d{0,2})?$", ErrorMessage = "Invalid balance format.")]
    public decimal Balance;

    [Required(ErrorMessage = "Balance is required.")]
    [Range(-99999999.99, 99999999.99, ErrorMessage = "Balance must be between -99,999,999.99 and 99,999,999.99")]
    public required string BalanceStr
    {
        get => Balance.ToString("N2", CultureInfo.InvariantCulture);
        set
        {
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedBalance))
            {
                Balance = parsedBalance;
            }
            else
            {
                throw new FormatException("Balance must be a valid decimal number.");
            }
        }
    }
}