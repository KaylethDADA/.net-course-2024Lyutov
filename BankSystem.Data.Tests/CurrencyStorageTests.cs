using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Tests
{
    public class CurrencyStorageTests
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly CurrencyStorage _currencyStorage;
        private readonly TestDataGenerator _testDataGenerator;

        public CurrencyStorageTests()
        {
            _dbContext = new BankSystemDbContext();
            _currencyStorage = new CurrencyStorage(_dbContext);
            _testDataGenerator = new TestDataGenerator();
        }

        [Fact]
        public void AddCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();

            // Act
            _currencyStorage.Add(currency);
            var result = _dbContext.Currencies.FirstOrDefault(x => x.Id == currency.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            _currencyStorage.Add(currency);

            // Act
            currency.Description = "UpEuro";
            _currencyStorage.Update(currency);
            var result = _dbContext.Currencies.FirstOrDefault(x => x.Id == currency.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpEuro", result.Description);
        }

        [Fact]
        public void GetDefaultCurrencyCurrencyPositiveTest()
        {
            // Arrange
            var defaultCurrency = new Currency
            {
                Id = Guid.NewGuid(),
                Code = "USD",
                Description = "US Dollar",
                Symbol = "$"
            };

            _currencyStorage.Add(defaultCurrency);

            // Act
            var result = _currencyStorage.GetDefaultCurrency();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("USD", result.Code);
        }

        [Fact]
        public void GetById()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            _currencyStorage.Add(currency);

            // Act
            var result = _currencyStorage.GetById(currency.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(currency, result);
        }

        [Fact]
        public void DeleteCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            _currencyStorage.Add(currency);

            // Act
            _currencyStorage.Delete(currency.Id);
            var result = _currencyStorage.GetById(currency.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
