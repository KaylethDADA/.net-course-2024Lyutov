using BankSystem.Domain.Models;

namespace BankSystem.Application.Interfaces
{
    public interface IClientStorage : IStorage<Client>
    {
        Client GetById(Guid Id);
        ICollection<Account> GetAccountsByClientId(Guid clientId);
        void AddAccount(Guid clientId, Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Guid accountId);
    }
}
