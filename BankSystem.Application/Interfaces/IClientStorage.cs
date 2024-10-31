using BankSystem.Domain.Models;

namespace BankSystem.Application.Interfaces
{
    public interface IClientStorage : IStorage<Client>
    {
        Task<ICollection<Account>> GetAccountsByClientIdAsync(Guid clientId, CancellationToken cancellationToken);
        Task AddAccountAsync(Guid clientId, Account account, CancellationToken cancellationToken);
        Task UpdateAccountAsync(Account account, CancellationToken cancellationToken);
        Task DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken);
        Task<Client> GetByPassportNumberAsync(string passportNumber, CancellationToken cancellationToken);
        Task<ICollection<Account>> GetAllAccount(CancellationToken cancellationToken);
    }
}
