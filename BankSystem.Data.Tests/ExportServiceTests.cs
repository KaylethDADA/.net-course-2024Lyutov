using BankSystem.Application.Services;
using BankSystem.Domain.Models;
using ExportTool;

namespace BankSystem.Data.Tests
{
    public class ExportServiceTests
    {
        private readonly TestDataGenerator _testDataGenerator;
        private readonly string _testDirectory;
        private readonly string _testFileCSVPath;

        public ExportServiceTests()
        {
            _testDataGenerator = new TestDataGenerator();

            _testDirectory = "TestCsv";
            _testFileCSVPath = Path.Combine(_testDirectory, "test_clients.csv");

            if (!Directory.Exists(_testDirectory))
                Directory.CreateDirectory(_testDirectory);
        }

        [Fact]
        public void ExportClientsToCsvPositiveTest()
        {
            var exportService = new ExportService<Client>();

            // Arrange
            var clients = _testDataGenerator.GenerateClients(5);

            // Act
            exportService.ExportEntitiesToCsv(clients, _testFileCSVPath);

            // Assert
            Assert.True(File.Exists(_testFileCSVPath));
            var exportedClients = exportService.ImportEntitiesFromCsv(_testFileCSVPath);
            Assert.True(exportedClients.Count > 1);
            Assert.Equal(clients, exportedClients);
        }

        [Fact]
        public void ImportClientsFromCsvPositiveTest()
        {
            var exportService = new ExportService<Client>();

            // Arrange
            var clients = _testDataGenerator.GenerateClients(5);
            exportService.ExportEntitiesToCsv(clients, _testFileCSVPath);

            // Act
            var importedClients = exportService.ImportEntitiesFromCsv(_testFileCSVPath);

            // Assert
            Assert.NotNull(importedClients);
            Assert.Equal(clients, importedClients);
            Assert.Equal(clients.Count(), importedClients.Count());
        }

        [Fact]
        public void ExportClientsToJsonPositiveTest()
        {
            var exportService = new ExportService<Client>();

            // Arrange
            var clients = _testDataGenerator.GenerateClients(5);
            var testJsonPathClients = Path.Combine(_testDirectory, "test_clients.json");

            // Act
            exportService.ExportEntitiesToJson(clients, testJsonPathClients);

            // Assert
            Assert.True(File.Exists(testJsonPathClients));
            var importedClients = exportService.ImportEntitiesFromJson(testJsonPathClients);
            Assert.NotNull(importedClients);
            Assert.Equal(clients, importedClients);
            Assert.Equal(clients.Count(), importedClients.Count());
        }

        [Fact]
        public void ImportClientsFromJsonPositiveTest()
        {
            var exportService = new ExportService<Client>();

            // Arrange
            var clients = _testDataGenerator.GenerateClients(5);
            var testJsonPath = Path.Combine(_testDirectory, "test_clients.json");
            exportService.ExportEntitiesToJson(clients, testJsonPath);

            // Act
            var importedClients = exportService.ImportEntitiesFromJson(testJsonPath);

            // Assert
            Assert.NotNull(importedClients);
            Assert.Equal(clients, importedClients);
            Assert.Equal(clients.Count(), importedClients.Count());
        }

        [Fact]
        public void ExportEmployeesToJsonPositiveTest()
        {
            var exportService = new ExportService<Employee>();

            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(5);
            var testJsonPathEmployee = Path.Combine(_testDirectory, "test_employees.json");

            // Act
            exportService.ExportEntitiesToJson(employees, testJsonPathEmployee);

            // Assert
            Assert.True(File.Exists(testJsonPathEmployee));
            var importedEmployees = exportService.ImportEntitiesFromJson(testJsonPathEmployee);
            Assert.NotNull(importedEmployees);
            Assert.Equal(employees, importedEmployees);
            Assert.Equal(employees.Count(), importedEmployees.Count());
        }

        [Fact]
        public void ImportEmployeesFromJsonPositiveTest()
        {
            var exportService = new ExportService<Employee>();

            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(5);
            var testJsonPath = Path.Combine(_testDirectory, "test_employees.json");
            exportService.ExportEntitiesToJson(employees, testJsonPath);

            // Act
            var importedEmployee = exportService.ImportEntitiesFromJson(testJsonPath);

            // Assert
            Assert.NotNull(importedEmployee);
            Assert.Equal(employees, importedEmployee);
            Assert.Equal(employees.Count(), importedEmployee.Count());
        }

        [Fact]
        public void ExportClientToJsonPositiveTest()
        {
            var exportService = new ExportService<Client>();

            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var testJsonPathClient = Path.Combine(_testDirectory, "test_client.json");

            // Act
            exportService.ExportEntityToJson(client, testJsonPathClient);

            // Assert
            var exportedClient = exportService.ImportEntityFromJson(testJsonPathClient);
            Assert.NotNull(exportedClient);
            Assert.Equal(client, exportedClient);
            Assert.Equal(client.FullName, exportedClient.FullName);
        }

        [Fact]
        public void ImportClientFromJsonPositiveTest()
        {
            var exportService = new ExportService<Client>();

            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var testJsonPathClient = Path.Combine(_testDirectory, "test_client.json");
            exportService.ExportEntityToJson(client, testJsonPathClient);

            // Act
            var importedClient = exportService.ImportEntityFromJson(testJsonPathClient);

            // Assert
            Assert.NotNull(importedClient);
            Assert.Equal(client, importedClient);
            Assert.Equal(client.FullName, importedClient.FullName);
        }
    }
}
