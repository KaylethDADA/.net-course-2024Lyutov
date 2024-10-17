using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
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

        public void Add(Currency item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }

        public void Update(Currency item)
        {
            var currency = _dbContext.Currencies.FirstOrDefault(x => x.Id == item.Id);
            if (currency == null)
                throw new Exception($"{nameof(Currency)} not found.");

            currency.Description = item.Description;
            currency.Code = item.Code;
            currency.Symbol = item.Symbol;

            _dbContext.SaveChanges();
        }

        public ICollection<Currency> Get(Expression<Func<Currency, bool>> filter, int pageNumber, int pageSize)
        {
            var currencies = _dbContext.Currencies.Where(filter).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return currencies;
        }

        public Currency? GetById(Guid Id)
        {
            return _dbContext.Currencies.FirstOrDefault(x =>x.Id == Id);
        }

        public Currency GetDefaultCurrency()
        {
            var currency = _dbContext.Currencies.FirstOrDefault(x => x.Code == "USD");
            if (currency == null)
                throw new Exception("Default currency not found.");

            return currency;
        }

        public void Delete(Guid id)
        {
            var currency = _dbContext.Currencies.FirstOrDefault(x => x.Id == id);
            if (currency == null)
                throw new Exception($"{nameof(Currency)} not found.");

            _dbContext.Currencies.Remove(currency);
            _dbContext.SaveChanges();
        }
    }
}
