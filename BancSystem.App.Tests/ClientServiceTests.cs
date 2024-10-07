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
        public void AddEmployeePositiveTest()
        {
            // Arrange
            var clients = _testDataGenerator.GenerateClients(10);

            // Act
            foreach (var employee in clients)
            {
                _clientService.AddClient(employee);
            }

            var actualClient = _clientService.Get(null);

            // Assert
            Assert.NotNull(actualClient);
            Assert.True(clients.SequenceEqual(actualClient));
        }

        [Fact]
        public void AddAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var account = _testDataGenerator.GenerateAccounts(1).First();

            _clientService.AddClient(client);

            // Act
            _clientService.AddAccount(client, account);

            // Assert
            var accounts = _clientStorage.GetClientAccounts(client);
            Assert.Contains(account, accounts);
        }

        [Fact]
        public void UpdateClientPositiveTest()
        {
            // Arrange
            var oldClient = _testDataGenerator.GenerateClients(1).First();
            var updatedClient = new Client
            {
                PassportNumber = oldClient.PassportNumber,
                FullName = "Updated Name",
                BirthDay = oldClient.BirthDay.AddYears(1),
                PhoneNumber = "1234567890"
            };

            _clientService.AddClient(oldClient);

            // Act
            _clientService.UpdateClient(updatedClient);
            var actualEmployee = _clientService.Get(e => e.PassportNumber == updatedClient.PassportNumber)
                .FirstOrDefault();

            // Assert
            Assert.NotNull(actualEmployee);
            Assert.Equal(updatedClient.FullName, actualEmployee.FullName);
            Assert.Equal(updatedClient.BirthDay, actualEmployee.BirthDay);
            Assert.Equal(updatedClient.PhoneNumber, actualEmployee.PhoneNumber);
        }

        [Fact]
        public void UpdateAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var currency = new Currency("USD", "$", "Доллар США");
            var oldAccount = new Account { Currency = currency, Amount = 500 };
            var newAccount = new Account { Currency = currency, Amount = 1000 };

            _clientService.AddClient(client);
            _clientService.AddAccount(client, oldAccount);

            // Act
            _clientService.UpdateAccount(client, oldAccount, newAccount);

            // Assert
            var accounts = _clientStorage.GetClientAccounts(client);
            Assert.DoesNotContain(oldAccount, accounts);
            Assert.Contains(newAccount, accounts);
        }

        [Fact]
        public void GetAllClientPositiveTest()
        {
            // Arrange
            var clients = _testDataGenerator.GenerateClients(10);
            foreach (var employee in clients)
            {
                _clientService.AddClient(employee);
            }

            // Act
            var allClients = _clientService.Get(null);

            // Assert
            Assert.NotNull(allClients);
            Assert.Equal(clients.Count, allClients.Count);
            Assert.True(clients.SequenceEqual(allClients));
        }

        [Fact]
        public void DeleteClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            _clientService.AddClient(client);

            // Act
            _clientService.DeleteClient(client);

            // Assert
            var storedClients = _clientService.Get(null);
            Assert.DoesNotContain(client, storedClients);
        }

        [Fact]
        public void DeleteAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var account = new Account { Currency = new Currency("USD", "$", "Доллар США"), Amount = 1000 };

            _clientService.AddClient(client);
            _clientService.AddAccount(client, account);

            // Act
            _clientService.DeleteAccount(client, account);

            // Assert
            var accounts = _clientStorage.GetClientAccounts(client);
            Assert.DoesNotContain(account, accounts);
        }
    }
}
