using Microsoft.AspNetCore.Components;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TimeTrove.Client.Models;
using Moq.Protected;
using TimeTrove.Client.Service;
using System.Xml;

namespace TimeTrove.Client.Tests.BankAccountServiceTests;
public class BankAccountServiceTests
{
    private HttpClient CreateHttpClient(HttpMessageHandler handler)
    {
        return new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost/")
        };
    }
    

    [Fact]
    public async Task GetBankAccounts_ReturnsBankAccounts()
    {
        // Arrange

        var fakeBankAccounts = new List<BankAccountDTO>
        {
            new BankAccountDTO { Name = "Test Account 1", Balance = 1000 },
            new BankAccountDTO { Name = "Test Account 2", Balance = 2000 }
        };

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected() // Access protected method
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(fakeBankAccounts)
            });

        var httpClient = CreateHttpClient(handlerMock.Object);

        // Directly pass the base URI string
        var bankAccountService = new BankAccountService(httpClient);

        // Act
        var result = await bankAccountService.GetBankAccounts();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Test Account 1", result[0].Name);
        Assert.Equal(1000, result[0].Balance);
        Assert.Equal("Test Account 2", result[1].Name);
        Assert.Equal(2000, result[1].Balance);

        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        );
    }


    [Fact]
    public async Task GetBankAccounts_ReturnsEmptyList_WhenNoAccounts()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new List<BankAccountDTO>())
            });

        var httpClient = CreateHttpClient(handlerMock.Object);

        var bankAccountService = new BankAccountService(httpClient);

        // Act
        var result = await bankAccountService.GetBankAccounts();

        // Assert
        Assert.Empty(result);

        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public async Task PostNewBankAccount_ReturnsNewAccount()
    {
        // Arrange

        var newBankAccount = new BankAccountDTO
        {
            Name = "Test",
            Balance = 0,
        };

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Post &&
                    req.RequestUri == new Uri("http://localhost/api/BankAccount") &&
                                                  req.Content.ReadAsStringAsync().Result.Contains("Test")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(newBankAccount)
            });

        var httpClient = CreateHttpClient(handlerMock.Object);

        var bankAccountService = new BankAccountService(httpClient);

        // Act

        var result = await bankAccountService.PostNewBankAccountAsync(newBankAccount);

        //Arrange

        Assert.NotNull(result);
        Assert.Equal(newBankAccount, result);
        Assert.Equal("Test", result.Name);
        Assert.Equal(0, result.Balance);

    }

}
