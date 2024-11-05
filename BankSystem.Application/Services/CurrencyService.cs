using BankSystem.Application.Dto.Currency;
using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankSystem.Application.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _key = "6bTB9QskFCAVCwD3SdEHa8xUityY7n";
        private readonly ICurrencyStorage _currencyStorage;

        public CurrencyService(ICurrencyStorage currencyStorage, HttpClient httpClient)
        {
            _currencyStorage = currencyStorage;
            _httpClient = httpClient;
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

        public async Task<ICollection<Currency>> GetAsync(
            Expression<Func<Currency, bool>>? filter,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            return await _currencyStorage.GetAsync(filter, pageNumber, pageSize, cancellationToken);
        }

        public async Task<Currency> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _currencyStorage.GetByIdAsync(id, cancellationToken);
        }

        public async Task DeleteAddAsync(Guid id, CancellationToken cancellationToken)
        {
           await _currencyStorage.DeleteAsync(id, cancellationToken);
        }

        public async Task<CurrencyConvertResponse> ConvertCurrency(ConvertCurrencyRequest request, CancellationToken cancellationToken)
        {
            string uri = $"https://www.amdoren.com/api/currency.php?api_key={_key}&from={request.FromCurrency}&to={request.ToCurrency}&amount={request.Amount}";

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var response = await _httpClient.GetStringAsync(uri, cancellationToken);
            var result = JsonSerializer.Deserialize<CurrencyConvertResponse>(response, options);

            if (result.Error != 0)
                throw new Exception(result.ErrorMessage);

            return result;
        }
    }
}
