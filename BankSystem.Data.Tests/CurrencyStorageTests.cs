using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Tests
{
    public class CurrencyStorageTests
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly CurrencyStorage _currencyStorage;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly CancellationToken _token;

        public CurrencyStorageTests()
        {
            _dbContext = new BankSystemDbContext();
            _currencyStorage = new CurrencyStorage(_dbContext);
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
            var result = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == currency.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            await _currencyStorage.AddAsync(currency, _token);

            // Act
            currency.Description = "UpEuro";
            await _currencyStorage.UpdateAsync(currency, _token);
            var result = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == currency.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpEuro", result.Description);
        }

        [Fact]
        public async Task GetDefaultCurrencyCurrencyPositiveTest()
        {
            // Arrange
            var defaultCurrency = new Currency
            {
                Id = Guid.NewGuid(),
                Code = "USD",
                Description = "US Dollar",
                Symbol = "$"
            };

            await _currencyStorage.AddAsync(defaultCurrency, _token);

            // Act
            var result = await _currencyStorage.GetDefaultCurrencyAsync(_token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("USD", result.Code);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            await _currencyStorage.AddAsync(currency, _token);

            // Act
            var result = await _currencyStorage.GetByIdAsync(currency.Id, _token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(currency, result);
        }

        [Fact]
        public async Task DeleteCurrencyPositiveTest()
        {
            // Arrange
            var currency = _testDataGenerator.GenerateCurrencies(1).First();
            await _currencyStorage.AddAsync(currency, _token);

            // Act
            await _currencyStorage.DeleteAsync(currency.Id, _token);
            var result = await _currencyStorage.GetByIdAsync(currency.Id, _token);

            // Assert
            Assert.Null(result);
        }
    }
}
