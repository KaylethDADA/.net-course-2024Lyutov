using BankSystem.Application.Services;
using BankSystem.Data.Storages;

namespace BankSystem.Data.Tests
{
   /* public class RateUpdaterTests
    {
        private readonly RateUpdater _rateUpdater;
        private readonly ClientStorage _clientStorage;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly CancellationToken _token;

        public RateUpdaterTests()
        {
            _clientStorage = new ClientStorage(new BankSystemDbContext());
            _rateUpdater = new RateUpdater(_clientStorage);
            _testDataGenerator = new TestDataGenerator();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task ApplyMonthlyRateAsyncPositiveTest()
        {
            // Arrange
            decimal interestRate = 0.02m;
            var client = _testDataGenerator.GenerateClients(1).First();
            var accounts = _testDataGenerator.GenerateAccounts(10, _testDataGenerator.GenerateCurrencies(1));

            await _clientStorage.AddAsync(client, _token);
            foreach (var account in accounts)
            {
                account.LastUpdatedDate = DateTime.UtcNow.AddMonths(-1);
                await _clientStorage.AddAccountAsync(client.Id, account, _token);
            }

            // Act
            await _rateUpdater.ApplyMonthlyRateAsync(interestRate, _token);

            // Assert
            var updatedAccounts = await _clientStorage.GetAllAccount(_token);
            foreach (var account in updatedAccounts)
            {
                decimal expectedAmount = account.Amount / (1 + interestRate);
                expectedAmount += expectedAmount * interestRate;
                Assert.Equal(expectedAmount, account.Amount, precision: 2);
                Assert.Equal(DateTime.UtcNow.Date, account.LastUpdatedDate.Date);
            }
        }
    }*/
}
