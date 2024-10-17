using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Data;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class CurrencyServiceTests
    {
        private readonly ClientStorage _clientStorage;
        private readonly CurrencyStorage _currencyStorage;
        private readonly TestDataGenerator _testDataGenerator;

        public CurrencyServiceTests()
        {
            var dbContext = new BankSystemDbContext();
            _clientStorage = new ClientStorage(dbContext);
            _currencyStorage = new CurrencyStorage(dbContext);
            _testDataGenerator = new TestDataGenerator();
        }

        [Fact]
        public void AddCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();

            // Act
            _currencyStorage.Add(currency);

            // Assert
            var storedCurrency = _currencyStorage.GetById(currency.Id);
            Assert.NotNull(storedCurrency);
            Assert.Equal(currency.Code, storedCurrency.Code);
        }

        [Fact]
        public void UpdateCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            _currencyStorage.Add(currency);

            var updatedCurrency = new Currency
            {
                Id = currency.Id,
                Description = "UpDescription",
                Code = currency.Code,
                Symbol = currency.Symbol
            };

            // Act
            _currencyStorage.Update(updatedCurrency);
            var actualCurrency = _currencyStorage.GetById(currency.Id);

            // Assert
            Assert.NotNull(actualCurrency);
            Assert.Equal(updatedCurrency.Description, actualCurrency.Description);
        }

        [Fact]
        public void GetCurrencyByIdPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            _currencyStorage.Add(currency);

            // Act
            var actualCurrency = _currencyStorage.GetById(currency.Id);

            // Assert
            Assert.NotNull(actualCurrency);
            Assert.Equal(currency.Code, actualCurrency.Code);
        }

        [Fact]
        public void GetDefaultCurrencyPositiveTest()
        {
            // Arrange
            var defaultCurrency = new Currency
            {
                Id = Guid.NewGuid(),
                Description = "US Dollar",
                Code = "USD",
                Symbol = "$"
            };

            _currencyStorage.Add(defaultCurrency);

            // Act
            var actualDefaultCurrency = _currencyStorage.GetDefaultCurrency();

            // Assert
            Assert.NotNull(actualDefaultCurrency);
            Assert.Equal(defaultCurrency.Code, actualDefaultCurrency.Code);
        }

        [Fact]
        public void DeleteCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            _currencyStorage.Add(currency);

            // Act
            _currencyStorage.Delete(currency.Id);

            // Assert
            var deletedCurrency = _currencyStorage.GetById(currency.Id);
            Assert.Null(deletedCurrency);
        }
    }
}
