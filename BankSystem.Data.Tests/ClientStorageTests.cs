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

            var clients = testDataGenerator.GenerateClients(10);

            // Act
            foreach (var client in clients)
            {
                storage.AddClient(client);
            }
            var actualClients = storage.Clients;

            // Assert
            Assert.NotNull(actualClients);
            Assert.True(clients.SequenceEqual(actualClients));
        }

        [Fact]
        public void GetYoungestClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var Clients = testDataGenerator.GenerateClients(10);
            foreach (var Client in Clients)
            {
                storage.AddClient(Client);
            }

            // Act
            var youngest = storage.GetYoungestClient();
            var expectedYoungest = Clients.OrderBy(x => x.Age)
                    .FirstOrDefault();

            // Assert
            Assert.NotNull(expectedYoungest);
            Assert.Equal(expectedYoungest, youngest);
        }

        [Fact]
        public void GetOldestClientPositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var Clients = testDataGenerator.GenerateClients(10);
            foreach (var Client in Clients)
            {
                storage.AddClient(Client);
            }

            // Act
            var oldest = storage.GetOldestClient();
            var expectedOldest = Clients.OrderByDescending(x => x.Age)
                    .FirstOrDefault();

            // Assert
            Assert.NotNull(expectedOldest);
            Assert.Equal(expectedOldest, oldest);
        }

        [Fact]
        public void GetAverageAgePositiveTest()
        {
            // Arrange
            var storage = new ClientStorage();
            var testDataGenerator = new TestDataGenerator();

            var Clients = testDataGenerator.GenerateClients(10);
            foreach (var Client in Clients)
            {
                storage.AddClient(Client);
            }

            // Act
            var averageAge = storage.GetAverageAgeClient();
            var expectedAverageAge = Clients.Average(x => x.Age);

            // Assert
            Assert.Equal(expectedAverageAge, averageAge);
        }
    }
}