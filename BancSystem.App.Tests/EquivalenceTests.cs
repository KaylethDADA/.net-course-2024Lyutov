using BankSystem.Application.Services;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class EquivalenceTests
    {
        [Fact]
        public void GetHashCodeNecessityPositiveTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();
            
            var clients = testDataGenerator.GenerateClients(10);
            var clientAccountDictionary = testDataGenerator.GenerateClientAccounts(clients);

            var existingClient = clientAccountDictionary.Keys.First();
            var newClient = new Client
            {
               FullName = existingClient.FullName,
               BirthDay = existingClient.BirthDay,
               PhoneNumber = existingClient.PhoneNumber,
            };

            // Act
            bool account = clientAccountDictionary.ContainsKey(newClient);

            // Assert
            Assert.True(account);
        }

        [Fact]
        public void ClientHasMultipleAccountsTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();
            var clients = testDataGenerator.GenerateClients(5);
            var clientAccountDictionary = testDataGenerator.GenerateClientAccounts(clients);

            var clientWithAccounts = clients[0];

            // Act
            var accounts = clientAccountDictionary[clientWithAccounts];

            // Assert
            Assert.True(accounts.Count > 1);
        }

        [Fact]
        public void EmployeeExistsInListPositiveTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();
            var employees = testDataGenerator.GenerateEmployees(10);
            var existingEmployee = employees[0];

            // Act
            bool employeeExists = employees.Contains(existingEmployee);

            // Assert
            Assert.True(employeeExists);
        }
    }
}