using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(Client item, CancellationToken cancellationToken)
        {
            await _dbContext.Clients.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAccountAsync(Guid clientId, Account account, CancellationToken cancellationToken)
        {
            account.ClientId = clientId;

            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client item, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == item.Id);

            client.FirstName = item.FirstName;
            client.LastName = item.LastName;
            client.PhoneNumber = item.PhoneNumber;
            client.BirthDay = item.BirthDay;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account, CancellationToken cancellationToken)
        {
            var exAccount = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);

            if (exAccount == null)
                throw new Exception("Account not found.");

            exAccount.Amount = account.Amount;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<Account>> GetAllAccount(CancellationToken cancellationToken)
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<ICollection<Client>> GetAsync(
            Expression<Func<Client, bool>>? filter,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            var quer = _dbContext.Set<Client>().AsQueryable();

            if (filter != null)
                quer = quer.Where(filter);

            if (pageNumber != null && pageSize != null)
                quer = quer.Skip((pageNumber.Value - 1) * pageSize.Value)
                           .Take(pageSize.Value);

            return await quer.ToArrayAsync();
        }

        public async Task<Client>? GetByIdAsync(Guid clientId, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == clientId);
        }

        public async Task<ICollection<Account>> GetAccountsByClientIdAsync(Guid clientId, CancellationToken cancellationToken)
        {
            return await _dbContext.Accounts.Where(x => x.ClientId == clientId).ToArrayAsync();
        }

        public async Task<Client>? GetByPassportNumberAsync(string passportNumber, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.PassportNumber == passportNumber);
        }

        public async Task DeleteAsync(Guid clientidId, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == clientidId);
            if (client == null)
                throw new Exception($"{nameof(Client)} not found.");

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            if (account == null)
                throw new Exception($"{nameof(Account)} not found.");

            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}
