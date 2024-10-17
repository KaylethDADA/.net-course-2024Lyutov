using BankSystem.Application.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class ClientServiceTests
    {
        private readonly ClientStorage _clientStorage;
        private readonly CurrencyStorage _currencyStorage;
        private readonly ClientService _clientService;
        private readonly TestDataGenerator _testDataGenerator;

        public ClientServiceTests()
        {
            var dbContext = new BankSystemDbContext();
            _clientStorage = new ClientStorage(dbContext);
            _currencyStorage = new CurrencyStorage(dbContext);
            _clientService = new ClientService(_clientStorage, _currencyStorage);
            _testDataGenerator = new TestDataGenerator();
        }

        [Fact]
        public void AddClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            // Act
            _clientService.AddClient(client);

            // Assert
            var storedClient = _clientService.GetById(client.Id);

            Assert.NotNull(storedClient);
            Assert.Equal(client.PassportNumber, storedClient.PassportNumber);
        }

        [Fact]
        public void AddAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();

            _clientService.AddClient(client);

            // Act
            _clientService.AddAccount(client.Id, account);

            // Assert
            var accounts = _clientService.GetAccountsByClientId(client.Id);
            Assert.Contains(account, accounts);
        }

        [Fact]
        public void UpdateClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            _clientService.AddClient(client);
            var updatedClient = new Client
            {
                Id = client.Id, 
                FullName = new FullName { FirstName = "Jane", LastName = "Doe" },
                PassportNumber = client.PassportNumber,
                BirthDay = client.BirthDay,
                PhoneNumber = "0987654321"
            };

            // Act
            _clientService.UpdateClient(updatedClient);
            var actualClient = _clientService.GetById(client.Id);

            // Assert
            Assert.NotNull(actualClient);
            Assert.Equal(updatedClient.FullName, actualClient.FullName);
            Assert.Equal(updatedClient.PhoneNumber, actualClient.PhoneNumber);
        }

        [Fact]
        public void UpdateAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var oldAccount = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();
            var newAccount = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();

            _clientStorage.Add(client);
            _clientStorage.AddAccount(client.Id, oldAccount);

            newAccount.Id = oldAccount.Id;
            newAccount.ClientId = client.Id;

            // Act
            _clientService.UpdateAccount(newAccount);

            // Assert
            var accounts = _clientService.GetAccountsByClientId(client.Id);
            Assert.NotNull(accounts);
            Assert.Equal(accounts.First().Amount, newAccount.Amount);
        }

        [Fact]
        public void GetFilterClientPositiveTest()
        {
            // Arrange
            var clients = _testDataGenerator.GenerateClients(10);
            foreach (var client in clients)
            {
                _clientService.AddClient(client);
            }

            // Act
            var allClients = _clientService.Get(c => true, 1, 10);

            // Assert
            Assert.NotNull(allClients);
            Assert.Equal(clients.Count, allClients.Count);
        }

        [Fact]
        public void DeleteClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            _clientService.AddClient(client);

            // Act
            _clientService.DeleteClient(client.Id);

            // Assert
            var exClients = _clientService.GetById(client.Id);
            Assert.Null(exClients);
        }

        [Fact]
        public void DeleteAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();

            _clientService.AddClient(client);
            _clientService.AddAccount(client.Id, account);

            // Act
            _clientService.DeleteAccount(account.Id);

            // Assert
            var accounts = _clientService.GetAccountsByClientId(client.Id);
            Assert.DoesNotContain(account, accounts);
        }
    }
}
