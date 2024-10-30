using BankSystem.Application.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace BancSystem.App.Tests
{
    public class WithdrawAccountsTests
    {
        private readonly BankSystemDbContext _db;
        private readonly ClientService _clientService;
        private readonly CurrencyStorage _currencyStorage;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly ConcurrentQueue<(Guid clientId, Guid accountId, decimal amount)> _withdrawalQueue = new();
        private readonly CancellationToken _token;

        public WithdrawAccountsTests()
        {
            _db = new BankSystemDbContext();
            _currencyStorage = new CurrencyStorage(_db);
            _testDataGenerator = new TestDataGenerator();
            _clientService = new ClientService(new ClientStorage(_db), _currencyStorage);
            _token = new CancellationToken();
        }

        private async Task ProcessWithdrawalsAsync(ClientService clientService)
        {
            var tasks = new List<Task>();
            var startTime = DateTime.UtcNow;

            while (true)
            {
                if ((DateTime.UtcNow - startTime).TotalSeconds >= 30)
                    break;

                if (_withdrawalQueue.TryDequeue(out var withdrawal))
                {
                    tasks.Add(clientService.WithdrawAsync(withdrawal.clientId, withdrawal.accountId, withdrawal.amount, CancellationToken.None));

                    await Task.WhenAll(tasks);
                    tasks.Clear();
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }

        private void FillWithdrawalQueue(Guid clientId, List<Guid> accountIds, decimal amount)
        {
            foreach (var accountId in accountIds)
            {
                _withdrawalQueue.Enqueue((clientId, accountId, amount));
            }
        }

        [Fact]
        public async Task WithdrawFromAccountsAsyncTest()
        {
            // Arrange
            var client = _testDataGenerator.GenerateClients(1).First();
            var currency = await _currencyStorage.GetDefaultCurrencyAsync(_token);
            var accounts = _testDataGenerator.GenerateAccounts(10);

            await _clientService.AddClientAsync(client, _token);

            foreach (var account in accounts)
            {
                account.CurrencyId = currency.Id;
                account.Amount = 100;
                account.LastUpdatedDate = DateTime.UtcNow;
                await _clientService.AddAccountAsync(client.Id, account, _token);
            }

            // Act
            var tasks = new List<Task>();

            tasks.Add(Task.Run(() => FillWithdrawalQueue(client.Id, accounts.Select(a => a.Id).ToList(), 20)));
            tasks.Add(Task.Run(() => ProcessWithdrawalsAsync(_clientService)));

            await Task.WhenAll(tasks);

            // Assert
            foreach (var account in accounts)
            {
                var updatedAccount = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);
                Assert.Equal(80m, updatedAccount!.Amount);
            }
        }
    }
}
