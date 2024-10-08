﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Transactions;

namespace TimeTrove.Client.Models;

public class BankAccountDTO
{
    public int? Id { get; set; }
    [Required]
    [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "Name must be betwen 1-250 Charecters.")]
    public required string Name { get; set; }

    public decimal Balance { get; set; }

    public bool IsEditing { get; set; } = false;
  
    public BankAccountDTO()
    {
    }

    [SetsRequiredMembers]
    public BankAccountDTO(string name, decimal balance)
    {
        
        Name = name;
        Balance = balance;

    }
}