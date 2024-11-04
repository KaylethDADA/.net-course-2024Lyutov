using AutoMapper;
using BankSystem.Application.Dto.ClientDto;
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
        private readonly IMapper _mapper;

        public ClientService(IClientStorage clientStorage, ICurrencyStorage currencyStorage, IMapper mapper)
        {
            _clientStorage = clientStorage;
            _currencyStorage = currencyStorage;
            _mapper = mapper;
        }

        public async Task AddClientAsync(CreateClientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _mapper.Map<Client>(request);

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

        public async Task<ClientResponse> UpdateClientAsync(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _mapper.Map<Client>(request);
                await _clientStorage.UpdateAsync(client, cancellationToken);
                return _mapper.Map<ClientResponse>(client);
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

        public async Task<ICollection<ClientResponse>> GetAsync(GetClientFilterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var filter = _mapper.Map<Expression<Func<Client, bool>>>(request);
                var clients = await _clientStorage.GetAsync(filter, request.PageNumber, request.PageSize, cancellationToken);
                return _mapper.Map<ICollection<ClientResponse>>(clients);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while retrieving {nameof(Client)}.", ex);
            }
        }

        public async Task<ClientResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientStorage.GetByIdAsync(id, cancellationToken);
                return _mapper.Map<ClientResponse>(client);
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
