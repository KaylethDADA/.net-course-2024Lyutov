using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class CurrencyStorage : ICurrencyStorage
    {
        private readonly BankSystemDbContext _dbContext;

        public CurrencyStorage(BankSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Currency item, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Currency item, CancellationToken cancellationToken)
        {
            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (currency == null)
                throw new Exception($"{nameof(Currency)} not found.");

            currency.Description = item.Description;
            currency.Code = item.Code;
            currency.Symbol = item.Symbol;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Currency>> GetAsync(
            Expression<Func<Currency, bool>>? filter,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            var quer = _dbContext.Currencies.AsQueryable();

            if (filter != null)
                quer = quer.Where(filter);

            if (pageNumber != null && pageSize != null)
                quer = quer.Skip((pageNumber.Value - 1) * pageSize.Value)
                           .Take(pageSize.Value);

            return await quer.ToListAsync();
        }

        public async Task<Currency>? GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Currencies.FirstOrDefaultAsync(x =>x.Id == Id);
        }

        public async Task<Currency> GetDefaultCurrencyAsync(CancellationToken cancellationToken)
        {
            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Code == "USD");
            if (currency == null)
                throw new Exception("Default currency not found.");

            return currency;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == id);
            if (currency == null)
                throw new Exception($"{nameof(Currency)} not found.");

            _dbContext.Currencies.Remove(currency);
            await _dbContext.SaveChangesAsync();
        }
    }
}
