using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Application.Services
{
    public class CurrencyService
    {
        private readonly ICurrencyStorage _currencyStorage;

        public CurrencyService(ICurrencyStorage currencyStorage)
        {
            _currencyStorage = currencyStorage;
        }

        public void Add(Currency item)
        {
            if (item == null)
                throw new CurrencyValidationException("Currency item cannot be null.");

            if (string.IsNullOrWhiteSpace(item.Code))
                throw new CurrencyValidationException("Currency code is required.");

            if (string.IsNullOrWhiteSpace(item.Description))
                throw new CurrencyValidationException("Currency description is required.");

            if (string.IsNullOrWhiteSpace(item.Symbol))
                throw new CurrencyValidationException("Currency symbol is required.");

            _currencyStorage.Add(item);
        }

        public void Update(Currency item)
        {
            if (item == null)
                throw new CurrencyValidationException("Currency item cannot be null.");

            if (item.Id == Guid.Empty)
                throw new CurrencyValidationException("Invalid currency ID.");

            var exCurrency = _currencyStorage.GetById(item.Id);
            if (exCurrency == null)
                throw new CurrencyValidationException("Currency not found.");

            if (string.IsNullOrWhiteSpace(item.Code))
                throw new CurrencyValidationException("Currency code is required.");

            if (string.IsNullOrWhiteSpace(item.Description))
                throw new CurrencyValidationException("Currency description is required.");

            if (string.IsNullOrWhiteSpace(item.Symbol))
                throw new CurrencyValidationException("Currency symbol is required.");

            _currencyStorage.Update(item);
        }

        public ICollection<Currency> Get(Expression<Func<Currency, bool>> filter, int pageNumber, int pageSize)
        {
            return _currencyStorage.Get(filter, pageNumber, pageSize);
        }

        public Currency? GetById(Guid id)
        {
            return _currencyStorage.GetById(id);
        }

        public Currency GetDefaultCurrency()
        {
            return _currencyStorage.GetDefaultCurrency();
        }

        public void Delete(Guid id)
        {
            _currencyStorage.Delete(id);
        }
    }
}
