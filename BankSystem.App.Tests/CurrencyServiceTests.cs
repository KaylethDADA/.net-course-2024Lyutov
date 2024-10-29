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
        private readonly CancellationToken _token;

        public CurrencyServiceTests()
        {
            var dbContext = new BankSystemDbContext();
            _clientStorage = new ClientStorage(dbContext);
            _currencyStorage = new CurrencyStorage(dbContext);
            _testDataGenerator = new TestDataGenerator();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task AddCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();

            // Act
            await _currencyStorage.AddAsync(currency, _token);

            // Assert
            var storedCurrency = await _currencyStorage.GetByIdAsync(currency.Id, _token);
            Assert.NotNull(storedCurrency);
            Assert.Equal(currency.Code, storedCurrency.Code);
        }

        [Fact]
        public async Task UpdateCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            await _currencyStorage.AddAsync(currency, _token);

            var updatedCurrency = new Currency
            {
                Id = currency.Id,
                Description = "UpDescription",
                Code = currency.Code,
                Symbol = currency.Symbol
            };

            // Act
            await _currencyStorage.UpdateAsync(updatedCurrency, _token);
            var actualCurrency = await _currencyStorage.GetByIdAsync(currency.Id, _token);

            // Assert
            Assert.NotNull(actualCurrency);
            Assert.Equal(updatedCurrency.Description, actualCurrency.Description);
        }

        [Fact]
        public async Task GetCurrencyByIdPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            await _currencyStorage.AddAsync(currency, _token);

            // Act
            var actualCurrency = await _currencyStorage.GetByIdAsync(currency.Id, _token);

            // Assert
            Assert.NotNull(actualCurrency);
            Assert.Equal(currency.Code, actualCurrency.Code);
        }

        [Fact]
        public async Task GetDefaultCurrencyPositiveTest()
        {
            // Arrange
            var defaultCurrency = new Currency
            {
                Id = Guid.NewGuid(),
                Description = "US Dollar",
                Code = "USD",
                Symbol = "$"
            };

            await _currencyStorage.AddAsync(defaultCurrency, _token);

            // Act
            var actualDefaultCurrency = await _currencyStorage.GetDefaultCurrencyAsync(_token);

            // Assert
            Assert.NotNull(actualDefaultCurrency);
            Assert.Equal(defaultCurrency.Code, actualDefaultCurrency.Code);
        }

        [Fact]
        public async Task DeleteCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            await _currencyStorage.AddAsync(currency, _token);

            // Act
            await _currencyStorage.DeleteAsync(currency.Id, _token);

            // Assert
            var deletedCurrency = await _currencyStorage.GetByIdAsync(currency.Id, _token);
            Assert.Null(deletedCurrency);
        }
    }
}
