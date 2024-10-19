using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using ExportTool;

namespace BankSystem.Data.Tests
{
    public class ExportServiceTests
    {
        private readonly ExportService _exportService;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly BankSystemDbContext _dbContext;
        private readonly ClientStorage _clientStorage;
        private readonly string _testDirectory;
        private readonly string _testFilePath;

        public ExportServiceTests()
        {
            _exportService = new ExportService();
            _testDataGenerator = new TestDataGenerator();
            _dbContext = new BankSystemDbContext();
            _clientStorage = new ClientStorage(_dbContext);

            _testDirectory = "TestCsv";
            _testFilePath = Path.Combine(_testDirectory, "test_clients.csv");

            if (!Directory.Exists(_testDirectory))
                Directory.CreateDirectory(_testDirectory);
        }

        [Fact]
        public void ExportClientsToCsvPositiveTest()
        {
            // Arrange
            var clientsTest = _testDataGenerator.GenerateClients(5);

            foreach (var client in clientsTest) 
            {
                _clientStorage.Add(client);
            }

            var clients = _clientStorage.Get(x => true, 1, 5);

            // Act
            _exportService.ExportClientsToCsv(clients, _testFilePath);

            // Assert
            Assert.True(File.Exists(_testFilePath));
            var exportedClients = File.ReadAllLines(_testFilePath);
            Assert.True(exportedClients.Length > 1);
        }

        [Fact]
        public void ImportClientsFromCsvPositiveTest()
        {
            // Arrange
            var clients = _testDataGenerator.GenerateClients(5);
            _exportService.ExportClientsToCsv(clients, _testFilePath);

            // Act
            var importedClients = _exportService.ImportClientsFromCsv(_testFilePath);

            // Assert
            Assert.NotNull(importedClients);
            Assert.Equal(clients.Count(), importedClients.Count());
            Assert.True(importedClients.All(c => clients.Any(ec => ec.Id == c.Id)));
        }
    }
}
