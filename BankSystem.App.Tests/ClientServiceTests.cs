using BankSystem.Application.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class ClientServiceTests
    {
        private readonly ClientService _clientService;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly CancellationToken _token;

        public ClientServiceTests()
        {
            var dbContext = new BankSystemDbContext();
            _clientService = new ClientService(new ClientStorage(dbContext), new CurrencyStorage(dbContext));
            _testDataGenerator = new TestDataGenerator();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task AddClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            // Act
            await _clientService.AddClientAsync(client, _token);

            // Assert
            var storedClient = await _clientService.GetByIdAsync(client.Id, _token);

            Assert.NotNull(storedClient);
            Assert.Equal(client.PassportNumber, storedClient.PassportNumber);
        }

        [Fact]
        public async Task AddAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();

            await _clientService.AddClientAsync(client, _token);

            // Act
            await _clientService.AddAccountAsync(client.Id, account, _token);

            // Assert
            var accounts = await _clientService.GetAccountsByClientIdAsync(client.Id, _token);
            Assert.Contains(account, accounts);
        }

        [Fact]
        public async Task UpdateClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            await _clientService.AddClientAsync(client, _token);
            var updatedClient = new Client
            {
                Id = client.Id, 
                FirstName = "Jane",
                LastName = "Doe",
                PassportNumber = client.PassportNumber,
                BirthDay = client.BirthDay,
                PhoneNumber = "0987654321"
            };

            // Act
            await _clientService.UpdateClientAsync(updatedClient, _token);
            var actualClient = await _clientService.GetByIdAsync(client.Id, _token);

            // Assert
            Assert.NotNull(actualClient);
            Assert.Equal(updatedClient.PhoneNumber, actualClient.PhoneNumber);
        }

        [Fact]
        public async Task UpdateAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();

            await _clientService.AddClientAsync(client, _token);
            var exAccount = await _clientService.GetAccountsByClientIdAsync(client.Id, _token);
            var newAccount = exAccount.First();
            newAccount.Amount = 1000m;

            // Act
            await _clientService.UpdateAccountAsync(newAccount, _token);

            // Assert
            var accounts = await _clientService.GetAccountsByClientIdAsync(client.Id, _token);
            Assert.NotNull(accounts);
            Assert.Equal(accounts.First().Amount, newAccount.Amount);
        }

        [Fact]
        public async Task GetFilterClientPositiveTest()
        {
            // Arrange
            var clients = _testDataGenerator.GenerateClients(10);
            foreach (var client in clients)
            {
                await _clientService.AddClientAsync(client, _token);
            }

            // Act
            var allClients = await _clientService.GetAsync(c => true, 1, 10, _token);

            // Assert
            Assert.NotNull(allClients);
            Assert.Equal(clients.Count, allClients.Count);
        }

        [Fact]
        public async Task DeleteClientPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            await _clientService.AddClientAsync(client, _token);

            // Act
            await _clientService.DeleteClientAsync(client.Id, _token);

            // Assert
            var exClients = await _clientService.GetByIdAsync(client.Id, _token);
            Assert.Null(exClients);
        }

        [Fact]
        public async Task DeleteAccountPositiveTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();

            await _clientService.AddClientAsync(client, _token);
            await _clientService.AddAccountAsync(client.Id, account, _token);

            // Act
            await _clientService.DeleteAccountAsync(account.Id, _token);

            // Assert
            var accounts = await _clientService.GetAccountsByClientIdAsync(client.Id, _token);
            Assert.DoesNotContain(account, accounts);
        }
    }
}
