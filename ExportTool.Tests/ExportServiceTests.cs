using BankSystem.Application.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace ExportTool.Tests
{
    public class ExportServiceTests
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly ClientStorage _clientStorage;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly ExportService _exportService;

        public ExportServiceTests() 
        {
            _exportService = new ExportService();    
        }

        [Fact]
        public void ExportClientsToCsvPositiveTest()
        {
            // Arrange
            var clients = _dbContext.Clients.ToList();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "clients_export_test.csv");

            // Act
            _exportService.ExportClientsToCsv(clients, filePath);

            // Assert
            Assert.True(File.Exists(filePath));
            File.Delete(filePath);
        }

        [Fact]
        public void ImportClientsFromCsvPositiveTest()
        {

        }
    }
}