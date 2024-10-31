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

        public async Task AddAsync(Currency item, CancellationToken cancellationToken)
        {
            if (item == null)
                throw new CurrencyValidationException("Currency item cannot be null.");

            if (string.IsNullOrWhiteSpace(item.Code))
                throw new CurrencyValidationException("Currency code is required.");

            if (string.IsNullOrWhiteSpace(item.Description))
                throw new CurrencyValidationException("Currency description is required.");

            if (string.IsNullOrWhiteSpace(item.Symbol))
                throw new CurrencyValidationException("Currency symbol is required.");

            await _currencyStorage.AddAsync(item, cancellationToken);
        }

        public async Task UpdateAddAsync(Currency item, CancellationToken cancellationToken)
        {
            if (item == null)
                throw new CurrencyValidationException("Currency item cannot be null.");

            if (item.Id == Guid.Empty)
                throw new CurrencyValidationException("Invalid currency ID.");

            var exCurrency = await _currencyStorage.GetByIdAsync(item.Id, cancellationToken);
            if (exCurrency == null)
                throw new CurrencyValidationException("Currency not found.");

            if (string.IsNullOrWhiteSpace(item.Code))
                throw new CurrencyValidationException("Currency code is required.");

            if (string.IsNullOrWhiteSpace(item.Description))
                throw new CurrencyValidationException("Currency description is required.");

            if (string.IsNullOrWhiteSpace(item.Symbol))
                throw new CurrencyValidationException("Currency symbol is required.");

            await _currencyStorage.UpdateAsync(item, cancellationToken);
        }

        public async Task<ICollection<Currency>> GetAddAsync(
            Expression<Func<Currency, bool>>? filter,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            return await _currencyStorage.GetAsync(filter, pageNumber, pageSize, cancellationToken);
        }

        public async Task<Currency> GetByIdAddAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _currencyStorage.GetByIdAsync(id, cancellationToken);
        }

        public async Task DeleteAddAsync(Guid id, CancellationToken cancellationToken)
        {
           await _currencyStorage.DeleteAsync(id, cancellationToken);
        }
    }
}
