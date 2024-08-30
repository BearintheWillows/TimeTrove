using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using TimeTrove.Client.Models;

namespace TimeTrove.Client.Service
{
    public class BankAccountService
    {
        private readonly HttpClient _httpClient;

        public BankAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BankAccountDTO>> GetBankAccounts()
        {
            var response = await _httpClient.GetFromJsonAsync<List<BankAccountDTO>>(_httpClient.BaseAddress + "api/BankAccount");
            return response ?? new List<BankAccountDTO>();
        }

        public async Task SaveBankAccount(BankAccountDTO bankAccount)
        {
            // Add logic to save to the server
            await Task.CompletedTask; // Example: simulate saving
        }

        public async Task<BankAccountDTO> PostNewBankAccountAsync(BankAccountDTO newBankAccount)
        {
            // Add logic to send the new bank account to the server
            await Task.CompletedTask; // Example: simulate adding

            return newBankAccount;
        }
    }

}
