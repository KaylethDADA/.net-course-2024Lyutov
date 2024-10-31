using BankSystem.Application.Interfaces;

namespace BankSystem.Application.Services
{
    public class RateUpdater
    {
        private readonly IClientStorage _clientStorage;
        
        public RateUpdater(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public async Task ApplyMonthlyRateAsync(decimal interestRate, CancellationToken cancellationToken)
        {
            var accounts = await _clientStorage.GetAllAccount(cancellationToken);

            foreach (var account in accounts)
            {
                if ((DateTime.UtcNow - account.LastUpdatedDate).TotalDays >= 30)
                {
                    account.Amount += account.Amount * interestRate;
                    account.LastUpdatedDate = DateTime.UtcNow;
                    await _clientStorage.UpdateAccountAsync(account, cancellationToken);
                }
            }
        }
    }
}
