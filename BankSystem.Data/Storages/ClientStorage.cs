using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        private readonly BankSystemDbContext _dbContext;

        public ClientStorage(BankSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Client item)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.PassportNumber == item.PassportNumber);
            if (client != null)
                throw new Exception($"A {nameof(Client)} with the same passport number already exists.");

            _dbContext.Clients.Add(item);
            _dbContext.SaveChanges();
        }

        public void AddAccount(Guid clientId, Account account)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == clientId);
            if (client == null)
                throw new Exception($"{nameof(Client)} not found.");

            account.ClientId = clientId;
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        public void Update(Client item)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == item.Id);
            if (client == null)
                throw new Exception($"{nameof(Client)} not found.");

            client.FullName = item.FullName;
            client.PhoneNumber = item.PhoneNumber;
            client.BirthDay = item.BirthDay;

            _dbContext.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            var exAccount = _dbContext.Accounts.FirstOrDefault(x => x.Id == account.Id);
            if (exAccount == null)
                throw new Exception($"{nameof(Account)} not found.");

            exAccount.CurrencyName = account.CurrencyName;
            exAccount.Amount = account.Amount;

            _dbContext.SaveChanges();
        }

        public ICollection<Client> Get(Expression<Func<Client, bool>> filter, int pageNumber, int pageSize)
        {
            var clients = _dbContext.Clients.Where(filter).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return clients;
        }

        public Client GetById(Guid id)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == id);
            if (client == null)
                throw new Exception($"{nameof(Client)} not found.");

            return client;
        }

        public ICollection<Account> GetAccountsByClientId(Guid clientId)
        {
            return _dbContext.Accounts.Where(x => x.ClientId == clientId).ToList();
        }

        public void Delete(Guid id)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == id);

            if (client == null)
                throw new Exception($"{nameof(Client)} not found.");

            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Guid accountId)
        {
            var exAccount = _dbContext.Accounts.FirstOrDefault(x => x.Id == accountId);
            if (exAccount == null)
                throw new Exception($"{nameof(Account)} not found.");

            _dbContext.Accounts.Remove(exAccount);
            _dbContext.SaveChanges();
        }
    }
}
