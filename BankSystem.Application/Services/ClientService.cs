using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Application.Services
{
    public class ClientService
    {
        private readonly IClientStorage _clientStorage;
        private readonly ICurrencyStorage _currencyStorage;

        public ClientService(IClientStorage clientStorage, ICurrencyStorage currencyStorage)
        {
            _clientStorage = clientStorage;
            _currencyStorage = currencyStorage;
        }

        public async Task AddClientAsync(Client client, CancellationToken cancellationToken)
        {
            if (client.Age < 18)
                throw new ClientValidationException($"{nameof(Client)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(client.PassportNumber))
                throw new ClientValidationException($"The {nameof(Client)} must have passport details.");

            var result = await GetByPassportNumberAsync(client.PassportNumber, cancellationToken);
            if (result != null)
                throw new ClientValidationException($"A {nameof(Client)} with the same passport number already exists.");

            try
            {
                await _clientStorage.AddAsync(client, cancellationToken);

                var defaultAccount = new Account
                {
                    Currency = await _currencyStorage.GetDefaultCurrencyAsync(cancellationToken),
                    Amount = 0
                };

                await _clientStorage.AddAccountAsync(client.Id, defaultAccount, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while adding the {nameof(Client)}.", ex);
            }
        }

        public async Task AddAccountAsync(Guid clientId, Account account, CancellationToken cancellationToken)
        {
            var client = GetByIdAsync(clientId, cancellationToken);
            if (client == null)
                throw new ClientValidationException($"{nameof(Client)} not found.");

            try
            {
                await _clientStorage.AddAccountAsync(clientId, account, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while adding the account to the {nameof(Client)}.", ex);
            }
        }

        public async Task UpdateClientAsync(Client client, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ClientValidationException("The old or new client cannot be zero.");

            if (client.Age < 18)
                throw new ClientValidationException($"{nameof(Client)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(client.PassportNumber))
                throw new ClientValidationException($"The {nameof(Client)} must have passport details.");

            var result = GetByIdAsync(client.Id, cancellationToken);
            if (result == null)
                throw new ClientValidationException($"{nameof(Client)} not found.");

            try
            {
                await _clientStorage.UpdateAsync(client, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while editing the {nameof(Client)}.", ex);
            }
        }

        public async Task UpdateAccountAsync(Account account, CancellationToken cancellationToken)
        {
            if (account.Amount < 0)
                throw new ClientValidationException("The new account balance cannot be negative.");

            try
            {
               await _clientStorage.UpdateAccountAsync(account, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ClientException("An error occurred while editing the account.", ex);
            }
        }

        public async Task<ICollection<Client>> GetAsync(
            Expression<Func<Client, bool>>? filter,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            if (pageNumber <= 0)
                throw new ClientValidationException("Page number must be greater than zero.");

            if (pageSize <= 0)
                throw new ClientValidationException("Page size must be greater than zero.");

            try
            {
                return await _clientStorage.GetAsync(filter, pageNumber, pageSize, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while retrieving {nameof(Client)}.", ex);
            }
        }

        public async Task<Client> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _clientStorage.GetByIdAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving {nameof(Client)}.", ex);
            }
        }

        public async Task<ICollection<Account>> GetAccountsByClientIdAsync(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                return await _clientStorage.GetAccountsByClientIdAsync(clientId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving {nameof(Account)}.", ex);
            }
        }

        public async Task<Client> GetByPassportNumberAsync(string passportNumber, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(passportNumber))
                throw new ClientValidationException($"The {nameof(Client)} must have passport details.");
           
            return await _clientStorage.GetByPassportNumberAsync(passportNumber, cancellationToken);
        }

        public async Task DeleteClientAsync(Guid clientId, CancellationToken cancellationToken)
        {
            var client = await _clientStorage.GetByIdAsync(clientId, cancellationToken);
            if (client == null)
                throw new ClientValidationException($"{nameof(Client)} not found.");

            try
            {
                await _clientStorage.DeleteAsync(clientId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Client)}.", ex);
            }
        }

        public async Task DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken)
        {
            try
            {
                await _clientStorage.DeleteAccountAsync(accountId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Account)}.", ex);
            }
        }

        public async Task WithdrawAsync(Guid clientId, Guid accountId, decimal amount, CancellationToken cancellationToken)
        {
            var listAccount = await _clientStorage.GetAccountsByClientIdAsync(clientId, cancellationToken);
            var exAccount = listAccount.FirstOrDefault(x => x.Id == accountId);

            if (exAccount == null)
                throw new ClientValidationException("Account not found for the specified client");

            if (exAccount.Amount < amount)
                throw new ClientValidationException("Insufficient funds");

            exAccount.Amount -= amount;

            await _clientStorage.UpdateAccountAsync(exAccount, cancellationToken);
        }
    }
}
