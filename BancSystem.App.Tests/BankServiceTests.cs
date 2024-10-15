using BankSystem.Application.Services;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class BankServiceTests
    {
        private BankService _bankService;

        public BankServiceTests()
        {
            _bankService = new BankService();
        }

        [Fact]
        public void AddBonusPositiveTest()
        {
            // Arrange
            var client = new Client { FullName = new FullName { FirstName ="da", LastName = "afdad" } };
            decimal bonusAmount = 100.50m;

            // Act
            _bankService.AddBonus(client, bonusAmount);
            decimal bonus = _bankService.GetBonus(client);

            // Assert
            Assert.Equal(bonusAmount, bonus);
        }

        [Fact]
        public void AddToBlackListPositiveTest()
        {
            // Arrange
            var client = new Client { FullName = new FullName { FirstName = "da", LastName = "afdad" } };

            // Act
            _bankService.AddToBlackList(client);
            bool isInBlackList = _bankService.IsPersonInBlackList(client);

            // Assert
            Assert.True(isInBlackList);
        }

        [Fact]
        public void IsPersonInBlackListPositiveTest()
        {
            // Arrange
            var client = new Client { FullName = new FullName { FirstName = "da", LastName = "afdad" } };

            // Act
            bool isInBlackList = _bankService.IsPersonInBlackList(client);

            // Assert
            Assert.False(isInBlackList);
        }

        [Fact]
        public void CalculateOwnerSalaryPositiveTest()
        {
            // Arrange
            int bankProfit = 100000;
            int bankExpenses = 50000;
            int ownersCount = 2;

            // Act
            int salary = _bankService.CalculateOwnerSalary(bankProfit, bankExpenses, ownersCount);

            // Assert
            Assert.Equal(25000, salary);
        }

        [Fact]
        public void ConvertClientToEmployeePositiveTest()
        {
            // Arrange
            var client = new Client
            {
                FullName = new FullName { FirstName = "da", LastName = "afdad" },
                PhoneNumber = "123-456-7890",
                BirthDay = new DateTime(1990, 1, 1)
            };
            string contract = "C123";
            int salary = 30000;

            // Act
            var employee = _bankService.ConvertClientToEmployee(client, contract, salary);

            // Assert
            Assert.Equal(client.FullName, employee.FullName);
            Assert.Equal(client.PhoneNumber, employee.PhoneNumber);
            Assert.Equal(client.BirthDay, employee.BirthDay);
            Assert.Equal(contract, employee.Contract);
            Assert.Equal(salary, employee.Salary);
        }
    }
}
