using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeTrove.Client.Models;
using TimeTrove.Controllers;
using TimeTrove.Services;

namespace TimeTrove.Tests.BankAccountTests
{
    public class BankAccountControllerTests
    {

        private readonly Mock<IBankAccountService> _bankAccountServiceMock;
        private readonly BankAccountController _controller;

        public BankAccountControllerTests()
        {
            // Mock the BankAccountService
            _bankAccountServiceMock = new Mock<IBankAccountService>();

            // Instantiate the controller with the mocked service
            _controller = new BankAccountController(_bankAccountServiceMock.Object);

            // Optionally: Set up user claims if needed in the controller
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Email, "testuser@example.com"),
            }));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public async Task CreateBankAccount_ReturnsOk_WhenBankAccountIsCreated()
        {
            // Arrange
            var newAccount = new BankAccountDTO { Name = "Test Account", Balance = 1000 };
            _bankAccountServiceMock.Setup(service => service.CreateBankAccount(It.IsAny<BankAccountDTO>()))
                .ReturnsAsync(newAccount); // Simulate successful creation

            // Act
            var result = await _controller.CreateBankAccount(newAccount);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Check for OK result
            var returnedAccount = Assert.IsType<BankAccountDTO>(okResult.Value); // Check returned object
            Assert.Equal(newAccount.Name, returnedAccount.Name); // Validate that the returned data matches
            Assert.Equal(newAccount.Balance, returnedAccount.Balance);
        }

        [Fact]
        public async Task CreateBankAccount_ReturnsBadRequest_WhenBankAccountCreationFails()
        {
            // Arrange
            _bankAccountServiceMock.Setup(service => service.CreateBankAccount(It.IsAny<BankAccountDTO>()))
                .ReturnsAsync((BankAccountDTO?)null); // Simulate failure

            var newAccount = new BankAccountDTO { Name = "Test Account", Balance = 1000 };

            // Act
            var result = await _controller.CreateBankAccount(newAccount);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result); // Check for BadRequest result
            Assert.Equal("Failed to create bank account", badRequestResult.Value); // Validate message
        }

    }
}