using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Tests
{
    public class ClientStorageTests
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly ClientStorage _clientStorage;

        public ClientStorageTests()
        {
            _dbContext = new BankSystemDbContext();
            _clientStorage = new ClientStorage(_dbContext);
        }

        [Fact]
        public void AddClientPositiveTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();

            // Act
            _clientStorage.Add(client);

            // Assert
            var actualClient = _clientStorage.GetById(client.Id);

            Assert.NotNull(actualClient);
            Assert.Equal(client, actualClient);
        }

        [Fact]
        public void UpdateClientPositiveTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
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
        public void AddAccountPositiveTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            var account = testDataGenerator.GenerateAccounts(1).First();

            // Act
            _clientStorage.AddAccount(client.Id, account);

            // Assert
            var clientAccounts = _clientStorage.GetAccountsByClientId(client.Id);

            Assert.NotNull(clientAccounts);
            Assert.Contains(clientAccounts, a => a.Id == account.Id);
        }

        [Fact]
        public void UpdateAccountPositiveTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            var oldAccount = testDataGenerator.GenerateAccounts(1).First();
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
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            // Act
            var actualClient = _clientStorage.GetById(client.Id);

            // Assert
            Assert.NotNull(actualClient);
            Assert.Equal(client, actualClient);
        }

        [Fact]
        public void DeleteClientPositiveTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
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
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            _clientStorage.Add(client);

            var account = testDataGenerator.GenerateAccounts(1).First();
            _clientStorage.AddAccount(client.Id, account);

            // Act
            _clientStorage.DeleteAccount(account.Id);

            // Assert
            var deletedAccount = _dbContext.Accounts.FirstOrDefault(a => a.Id == account.Id);
            Assert.Null(deletedAccount);
        }

        [Fact]
        public void FilterClientsPositiveTest()
        {
            // Arrange
            var testDataGenerator = new TestDataGenerator();

            var client1 = testDataGenerator.GenerateClients(1).First();
            var client2 = testDataGenerator.GenerateClients(1).First();

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
            var testDataGenerator = new TestDataGenerator();
            var client = testDataGenerator.GenerateClients(1).First();

            _clientStorage.Add(client);

            var account1 = new Account { Id = Guid.NewGuid(), ClientId = client.Id, CurrencyName = "USD", Amount = 100 };
            var account2 = new Account { Id = Guid.NewGuid(), ClientId = client.Id, CurrencyName = "EUR", Amount = 200 };

            _clientStorage.AddAccount(client.Id, account1);
            _clientStorage.AddAccount(client.Id, account2);

            // Act
            var result = _clientStorage.GetAccountsByClientId(client.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, a => a.CurrencyName == "USD" && a.Amount == 100);
            Assert.Contains(result, a => a.CurrencyName == "EUR" && a.Amount == 200);
        }
    }
}