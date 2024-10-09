using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

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
            storage.Add(client);

            // Assert
            var actualClient = storage.Get(c => c.PassportNumber == client.PassportNumber).FirstOrDefault();

            Assert.NotNull(actualClient);
            Assert.Equal(client, actualClient);
        }

        [Fact]
        public void UpdateClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var oldClient = testDataGenerator.GenerateClients(1).First();
            storage.Add(oldClient);

            oldClient.FullName = "dadadawf";
            oldClient.PhoneNumber = "88800553535";

            var newClient = oldClient;

            // Act
            storage.Update(newClient);

            // Assert
            var actualClient = storage.Get(c => c.PassportNumber == newClient.PassportNumber).FirstOrDefault();

            Assert.NotNull(actualClient);
            Assert.Equal(newClient.FullName, actualClient?.FullName);
        }


        [Fact]
        public void UpdateAccountPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            storage.Add(client);

            var oldAccount = testDataGenerator.GenerateAccounts(1).First();
            storage.AddAccount(client, oldAccount);

            var newAccount = testDataGenerator.GenerateAccounts(1).First();

            // Act
            storage.UpdateAccount(client, oldAccount, newAccount);
            var clientAccounts = storage.Get(c => c.PassportNumber == client.PassportNumber).FirstOrDefault();

            // Assert
            Assert.NotNull(clientAccounts);
            Assert.DoesNotContain(oldAccount, storage.GetClientAccounts(client));
            Assert.Contains(newAccount, storage.GetClientAccounts(client));
        }

        [Fact]
        public void AddAccountPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            storage.Add(client);

            var newAccount = testDataGenerator.GenerateAccounts(1).First();

            // Act
            storage.AddAccount(client, newAccount);

            // Assert
            var clientAccounts = storage.GetClientAccounts(client);

            Assert.NotNull(clientAccounts);
            Assert.Contains(newAccount, clientAccounts);
        }

        [Fact]
        public void GetAllPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var expectedClient = testDataGenerator.GenerateClients(1).First();
            storage.Add(expectedClient);

            // Act
            var actualClients = storage.Get(null);

            // Assert
            Assert.NotNull(actualClients);
            Assert.Contains(expectedClient, actualClients);
        }

        [Fact]
        public void GetClientAccountsPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            storage.Add(client);

            var accounts = testDataGenerator.GenerateAccounts(10);

            foreach (var account in accounts)
            {
                storage.AddAccount(client, account);
            }

            // Act
            var clientAccounts = storage.GetClientAccounts(client);

            // Assert
            Assert.NotNull(clientAccounts);
            Assert.Equal(accounts.Count, clientAccounts.Count);
            Assert.True(accounts.SequenceEqual(clientAccounts));
        }

        [Fact]
        public void FilterClientsPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client1 = new Client { FullName = "John Doe", PhoneNumber = "1234567890", BirthDay = new DateTime(1990, 1, 1), PassportNumber = "123" };
            var client2 = new Client { FullName = "Jane Doe", PhoneNumber = "0987654321", BirthDay = new DateTime(1995, 1, 1), PassportNumber = "456" };

            storage.Add(client1);
            storage.Add(client2);

            // Act
            var result = storage.Get(c => c.FullName.Contains("John"));

            // Assert
            Assert.Single(result);
            Assert.Contains(client1, result);
        }

        [Fact]
        public void DeleteClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            storage.Add(client);

            // Act
            storage.Delete(client);

            // Assert
            var actualClient = storage.Get(c => c.PassportNumber == client.PassportNumber);
            Assert.Empty(actualClient);
        }

        [Fact]
        public void DeleteAccountPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var client = testDataGenerator.GenerateClients(1).First();
            storage.Add(client);

            var account = testDataGenerator.GenerateAccounts(1).First();
            storage.AddAccount(client, account);

            // Act
            storage.DeleteAccount(client, account);

            // Assert
            var clientAccounts = storage.Get(c => c.PassportNumber == client.PassportNumber)
                                        .SelectMany(c => storage.GetClientAccounts(c));

            Assert.DoesNotContain(account, clientAccounts);
        }
    }
}