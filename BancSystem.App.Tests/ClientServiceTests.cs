using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class ClientServiceTests
    {
        private readonly ClientStorage _clientStorage;
        private readonly ClientService _clientService;
        private readonly TestDataGenerator _testDataGenerator;

        public ClientServiceTests()
        {
            _testDataGenerator = new TestDataGenerator();
            _clientStorage = new ClientStorage();
            _clientService = new ClientService(_clientStorage);
        }

        [Fact]
        public void AddClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            // Act
            _clientService.AddClient(client);

            // Assert
            var storedClients = _clientStorage.GetAll().Keys.ToList();
            var accounts = _clientStorage.GetClientAccounts(client);

            Assert.Contains(client, storedClients);
            Assert.Single(accounts);
        }

        [Fact]
        public void AddAccountToClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var account = _testDataGenerator.GenerateAccounts(1).First();

            _clientStorage.AddClient(client);

            // Act
            _clientService.AddAccountToClient(client, account);

            // Assert
            var accounts = _clientStorage.GetClientAccounts(client);
            Assert.Contains(account, accounts);
        }

        [Fact]
        public void EditClientPositiveTest()
        {
            // Arrange
            var oldClient = _testDataGenerator.GenerateClients(1).First();
            var newClient = _testDataGenerator.GenerateClients(1).First();

            _clientStorage.AddClient(oldClient);

            // Act
            _clientService.EditClient(oldClient, newClient);

            // Assert
            var storedClients = _clientStorage.GetAll().Keys.ToList();
            Assert.DoesNotContain(oldClient, storedClients);
            Assert.Contains(newClient, storedClients);
        }

        [Fact]
        public void EditAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var currency = new Currency("USD", "$", "Доллар США");
            var oldAccount = new Account { Currency = currency, Amount = 500 };
            var newAccount = new Account { Currency = currency, Amount = 1000 };

            _clientStorage.AddClient(client);
            _clientStorage.AddAccountToClient(client, oldAccount);

            // Act
            _clientService.EditAccount(client, oldAccount, newAccount);

            // Assert
            var accounts = _clientStorage.GetClientAccounts(client);

            Assert.DoesNotContain(oldAccount, accounts);
            Assert.Contains(newAccount, accounts);
        }

        [Fact]
        public void FilterClientsPositiveTest()
        {
            // Arrange
            var client1 = new Client { FullName = "John Doe", PhoneNumber = "1234567890", BirthDay = new DateTime(1990, 1, 1), PassportNumber = "123" };
            var client2 = new Client { FullName = "Jane Doe", PhoneNumber = "0987654321", BirthDay = new DateTime(1995, 1, 1), PassportNumber = "456" };

            _clientStorage.AddClient(client1);
            _clientStorage.AddClient(client2);

            // Act
            var result = _clientService.FilterClients("John", null, null, null, null);

            // Assert
            Assert.Single(result);
            Assert.Contains(client1, result);
        }
    }
}
