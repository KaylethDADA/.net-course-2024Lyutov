using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Tests
{
   /* public class ClientStorageTests
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly ClientStorage _clientStorage;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly CancellationToken _token;

        public ClientStorageTests()
        {
            _dbContext = new BankSystemDbContext();
            _clientStorage = new ClientStorage(_dbContext);
            _testDataGenerator = new TestDataGenerator();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task AddClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            // Act
            await _clientStorage.AddAsync(client, _token);

            // Assert
            var actualClient = await _clientStorage.GetByIdAsync(client.Id, _token);

            Assert.NotNull(actualClient);
            Assert.Equal(client, actualClient);
        }

        [Fact]
        public async Task AddAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            await _clientStorage.AddAsync(client, _token);

            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();

            // Act
            await _clientStorage.AddAccountAsync(client.Id, account, _token);

            // Assert
            var clientAccounts = await _clientStorage.GetAccountsByClientIdAsync(client.Id, _token);

            Assert.NotNull(clientAccounts);
            Assert.Contains(clientAccounts, a => a.Id == account.Id);
        }

        [Fact]
        public async Task UpdateClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            await _clientStorage.AddAsync(client, _token);

            var upClient = new Client
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PassportNumber = client.PassportNumber,
                BirthDay = client.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
            };

            // Act
            await _clientStorage.UpdateAsync(upClient, _token);

            // Assert
            var actualClient = await _clientStorage.GetByIdAsync(upClient.Id, _token);
            Assert.NotNull(actualClient);
            Assert.Equal(upClient.BirthDay, client.BirthDay);
            Assert.Equal(upClient.PhoneNumber, client.PhoneNumber);
        }

        [Fact]
        public async Task UpdateAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            await _clientStorage.AddAsync(client, _token);

            var oldAccount = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();
            await _clientStorage.AddAccountAsync(client.Id, oldAccount, _token);

            oldAccount.Amount = 2000;

            // Act
            await _clientStorage.UpdateAccountAsync(oldAccount, _token);

            // Assert
            var updatedAccount = _clientStorage.GetAccountsByClientIdAsync(client.Id, _token).Result
                .FirstOrDefault(a => a.Id == oldAccount.Id);

            Assert.NotNull(updatedAccount);
            Assert.Equal(2000, updatedAccount.Amount);
        }

        [Fact]
        public async Task GetByIdClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            await _clientStorage.AddAsync(client, _token);

            // Act
            var actualClient = await _clientStorage.GetByIdAsync(client.Id, _token);

            // Assert
            Assert.NotNull(actualClient);
            Assert.Equal(client, actualClient);
        }

        [Fact]
        public async Task GetFilterClientsPositiveTest()
        {
            // Arrange
            var client1 = _testDataGenerator.GenerateClients(1).First();
            var client2 = _testDataGenerator.GenerateClients(1).First();

            await _clientStorage.AddAsync(client1, _token);
            await _clientStorage.AddAsync(client2, _token);

            // Act
            var result = await _clientStorage.GetAsync(c => c.FirstName.Contains(client1.FirstName), 1, 10, _token);

            // Assert
            Assert.Single(result);
            Assert.Contains(client1, result);
        }

        [Fact]
        public async Task GetAccountsByClientIdPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            await _clientStorage.AddAsync(client, _token);

            var accounts = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(5));

            foreach (var account in accounts)
            {
               await _clientStorage.AddAccountAsync(client.Id, account, _token);
            }

            // Act
            var result = await _clientStorage.GetAccountsByClientIdAsync(client.Id, _token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(accounts.Count, result.Count);
        }

        [Fact]
        public async Task GetByPassportNumberPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            await _clientStorage.AddAsync(client, _token);

            // Act
            var result = await _clientStorage.GetByPassportNumberAsync(client.PassportNumber, _token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, client);
        }

        [Fact]
        public async Task DeleteClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            await _clientStorage.AddAsync(client, _token);

            // Act
            await _clientStorage.DeleteAsync(client.Id, _token);

            // Assert
            var deletedClient = await _clientStorage.GetAsync(c => c.Id == client.Id, null, null, _token);
            Assert.Null(deletedClient.FirstOrDefault());
        }

        [Fact]
        public async Task DeleteAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            await _clientStorage.AddAsync(client, _token);

            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();
            await _clientStorage.AddAccountAsync(client.Id, account, _token);

            // Act
            await _clientStorage.DeleteAccountAsync(account.Id, _token);

            // Assert
            var deletedAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id);
            Assert.Null(deletedAccount);
        }
    }*/
}