using BankSystem.Application.Services;
using BankSystem.Data.Storages;

namespace BankSystem.Data.Tests
{
    public class ClientStorageTests
    {
        [Fact]
        public void AddClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();

            // Act
            storage.AddClient(client);

            // Assert
            var actualClient = storage.GetClient(client);

            Assert.NotNull(actualClient);
            Assert.Equal(client, actualClient);
        }

        [Fact]
        public void EditClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var oldClient = testDataGenerator.GenerateClients(1).First();
            storage.AddClient(oldClient);

            var newClient = testDataGenerator.GenerateClients(1).First();

            // Act
            storage.EditClient(oldClient, newClient);

            // Assert
            var account = storage.GetClient(newClient);

            Assert.NotNull(account);
            Assert.True(storage.GetAll().ContainsKey(newClient));
            Assert.False(storage.GetAll().ContainsKey(oldClient));
        }

        [Fact]
        public void EditAccountPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            storage.AddClient(client);

            var oldAccount = testDataGenerator.GenerateAccounts(1).First();
            storage.AddAccountToClient(client, oldAccount);

            var newAccount = testDataGenerator.GenerateAccounts(1).First();

            // Act
            storage.EditAccount(client, oldAccount, newAccount);
            var clientAccounts = storage.GetClientAccounts(client);

            // Assert
            Assert.DoesNotContain(oldAccount, clientAccounts);
            Assert.Contains(newAccount, clientAccounts);
        }

        [Fact]
        public void AddAccountToClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var clients = testDataGenerator.GenerateClients(1);
            var client = clients.First();

            storage.AddClient(client);

            var newAccount = testDataGenerator.GenerateAccounts(1).First();

            // Act
            storage.AddAccountToClient(client, newAccount);

            // Assert
            var clientAccounts = storage.GetClientAccounts(client);
            
            Assert.NotNull(clientAccounts);
            Assert.Contains(newAccount, clientAccounts);
        }

        [Fact]
        public void GetClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var expectedClient = testDataGenerator.GenerateClients(1).First();
            storage.AddClient(expectedClient);

            // Act
            var actualClient = storage.GetClient(expectedClient);

            // Assert
            Assert.NotNull(actualClient);
            Assert.Equal(expectedClient, actualClient);
        }

        [Fact]
        public void GetYoungestClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var clients = testDataGenerator.GenerateClients(10);
            foreach (var client in clients)
            {
                storage.AddClient(client);
            }

            // Act
            var youngest = storage.GetYoungestClient();

            // Assert
            var expectedYoungest = clients.OrderBy(x => x.Age).FirstOrDefault();

            Assert.NotNull(expectedYoungest);
            Assert.Equal(expectedYoungest, youngest);
        }

        [Fact]
        public void GetOldestClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var clients = testDataGenerator.GenerateClients(10);
            foreach (var client in clients)
            {
                storage.AddClient(client);
            }

            // Act
            var oldest = storage.GetOldestClient();

            // Assert
            var expectedOldest = clients.OrderByDescending(x => x.Age).FirstOrDefault();

            Assert.NotNull(expectedOldest);
            Assert.Equal(expectedOldest, oldest);
        }

        [Fact]
        public void GetAverageAgePositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var clients = testDataGenerator.GenerateClients(10);
            foreach (var client in clients)
            {
                storage.AddClient(client);
            }

            // Act
            var averageAge = storage.GetAverageAgeClient();

            // Assert
            var expectedAverageAge = clients.Average(x => x.Age);

            Assert.Equal(expectedAverageAge, averageAge);
        }

        [Fact]
        public void GetClientAccountsPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            storage.AddClient(client);

            var accounts = testDataGenerator.GenerateAccounts(10);

            foreach (var account in accounts)
            {
                storage.AddAccountToClient(client, account);
            }

            // Act
            var clientAccounts = storage.GetClientAccounts(client);

            // Assert
            Assert.NotNull(clientAccounts);
            Assert.Equal(accounts.Count, clientAccounts.Count);
            Assert.True(accounts.SequenceEqual(clientAccounts));
        }
    }
}