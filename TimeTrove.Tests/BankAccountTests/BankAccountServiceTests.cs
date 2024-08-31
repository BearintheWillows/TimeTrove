using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeTrove.Controllers;
using TimeTrove.Data.Models;
using TimeTrove.Data;
using TimeTrove.Services;
using TimeTrove.Client.Models;

namespace TimeTrove.Tests.BankAccountTests
{
    internal class BankAccountServiceTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly BankAccountService _service;

        public BankAccountServiceTests()
        {
            // Initialize mocks
            _contextMock = new Mock<ApplicationDbContext>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                null, null, null, null, null, null, null, null
            );
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Configure Service
            _service = new BankAccountService(_contextMock.Object, _userManagerMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task CreateBankAccount_Success_ReturnsBankAccountDTO()
        {
            // Arrange
            var userId = "user-id";
            var bankAccountDto = new BankAccountDTO
            {
                Name = "Test Account",
                Balance = 1000
            };

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
            _userManagerMock.Setup(um => um.GetUserId(httpContext.User)).Returns(userId);

            var bankAccount = new BankAccount();
            _contextMock.Setup(ctx => ctx.BankAccounts.AddAsync(It.IsAny<BankAccount>(), default))
                .ReturnsAsync(bankAccount);
            _contextMock.Setup(ctx => ctx.SaveChangesAsync(default))
                .ReturnsAsync(1); // Simulate successful save

            // Act
            var result = await _service.CreateBankAccount(bankAccountDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bankAccountDto.Name, result.Name);
            Assert.Equal(bankAccountDto.Balance, result.Balance);

            _contextMock.Verify(ctx => ctx.BankAccounts.AddAsync(It.IsAny<BankAccount>(), default), Times.Once);
            _contextMock.Verify(ctx => ctx.SaveChangesAsync(default), Times.Once);
        }

    }
}
