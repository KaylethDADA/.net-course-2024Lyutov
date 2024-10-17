using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Tests
{
    public class ClientStorageTests
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly ClientStorage _clientStorage;
        private readonly TestDataGenerator _testDataGenerator;

        public ClientStorageTests()
        {
            _dbContext = new BankSystemDbContext();
            _clientStorage = new ClientStorage(_dbContext);
            _testDataGenerator = new TestDataGenerator();
        }

        [Fact]
        public void AddClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            // Act
            _clientStorage.Add(client);

            // Assert
            var actualClient = _clientStorage.GetById(client.Id);

            Assert.NotNull(actualClient);
            Assert.Equal(client, actualClient);
        }

        [Fact]
        public void AddAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();

            // Act
            _clientStorage.AddAccount(client.Id, account);

            // Assert
            var clientAccounts = _clientStorage.GetAccountsByClientId(client.Id);

            Assert.NotNull(clientAccounts);
            Assert.Contains(clientAccounts, a => a.Id == account.Id);
        }

        [Fact]
        public void UpdateClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            var upClient = new Client
            {
                Id = client.Id,
                PassportNumber = client.PassportNumber,
                FullName = new FullName { FirstName = "UpName", LastName = "UpLName" },
                BirthDay = client.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
            };

            // Act
            _clientStorage.Update(upClient);

            // Assert
            var actualClient = _clientStorage.GetById(upClient.Id);
            Assert.NotNull(actualClient);
            Assert.Equal(upClient.FullName, client.FullName);
            Assert.Equal(upClient.BirthDay, client.BirthDay);
            Assert.Equal(upClient.PhoneNumber, client.PhoneNumber);
        }

        [Fact]
        public void UpdateAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            var oldAccount = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();
            _clientStorage.AddAccount(client.Id, oldAccount);

            oldAccount.Amount = 2000;

            // Act
            _clientStorage.UpdateAccount(oldAccount);

            // Assert
            var updatedAccount = _clientStorage.GetAccountsByClientId(client.Id)
                .FirstOrDefault(a => a.Id == oldAccount.Id);

            Assert.NotNull(updatedAccount);
            Assert.Equal(2000, updatedAccount.Amount);
        }

        [Fact]
        public void GetByIdClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            // Act
            var actualClient = _clientStorage.GetById(client.Id);

            // Assert
            Assert.NotNull(actualClient);
            Assert.Equal(client, actualClient);
        }

        [Fact]
        public void GetFilterClientsPositiveTest()
        {
            // Arrange
            var client1 = _testDataGenerator.GenerateClients(1).First();
            var client2 = _testDataGenerator.GenerateClients(1).First();

            _clientStorage.Add(client1);
            _clientStorage.Add(client2);

            // Act
            var result = _clientStorage.Get(c => c.FullName.FirstName.Contains(client1.FullName.FirstName), 1, 10);

            // Assert
            Assert.Single(result);
            Assert.Contains(client1, result);
        }

        [Fact]
        public void GetAccountsByClientIdPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            _clientStorage.Add(client);

            var accounts = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(5));

            foreach (var account in accounts)
            {
                _clientStorage.AddAccount(client.Id, account);
            }

            // Act
            var result = _clientStorage.GetAccountsByClientId(client.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(accounts.Count, result.Count);
        }

        [Fact]
        public void GetByPassportNumberPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            _clientStorage.Add(client);

            // Act
            var result = _clientStorage.GetByPassportNumber(client.PassportNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, client);
        }

        [Fact]
        public void DeleteClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            // Act
            _clientStorage.Delete(client.Id);

            // Assert
            var deletedClient = _dbContext.Clients.FirstOrDefault(c => c.Id == client.Id);
            Assert.Null(deletedClient);
        }

        [Fact]
        public void DeleteAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();
            _clientStorage.AddAccount(client.Id, account);

            // Act
            _clientStorage.DeleteAccount(account.Id);

            // Assert
            var deletedAccount = _dbContext.Accounts.FirstOrDefault(a => a.Id == account.Id);
            Assert.Null(deletedAccount);
        }
    }
}