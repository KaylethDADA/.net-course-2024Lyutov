using BankSystem.Domain.Models;

namespace BankSystem.Application.Interfaces
{
    public interface IClientStorage : IStorage<Client>
    {
        ICollection<Account> GetAccountsByClientId(Guid clientId);
        void AddAccount(Guid clientId, Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Guid accountId);
        Client? GetByPassportNumber(string passportNumber);
    }
}
