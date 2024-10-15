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
            _dbContext.Clients.Add(item);            
            _dbContext.SaveChanges();
        }

        public void AddAccount(Guid clientId, Account account)
        {
            account.ClientId = clientId;
            
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        public void Update(Client item)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == item.Id);

            client.FullName = item.FullName;
            client.PhoneNumber = item.PhoneNumber;
            client.BirthDay = item.BirthDay;

            _dbContext.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            var exAccount = _dbContext.Accounts.FirstOrDefault(x => x.Id == account.Id);

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

        public Client? GetById(Guid id)
        {
            return _dbContext.Clients.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Account> GetAccountsByClientId(Guid clientId)
        {
            return _dbContext.Accounts.Where(x => x.ClientId == clientId).ToList();
        }

        public Client? GetByPassportNumber(string passportNumber)
        {
            return _dbContext.Clients.FirstOrDefault(c => c.PassportNumber == passportNumber);
        }

        public void Delete(Guid id)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == id);

            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Guid accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.Id == accountId);
            if (account == null)
                throw new Exception($"{nameof(Account)} not found.");

            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
        }
    }
}
