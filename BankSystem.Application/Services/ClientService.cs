using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Application.Services
{
    public class ClientService
    {
        private readonly IClientStorage _clientStorage;

        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddClient(Client client)
        {
            if (client.Age < 18)
                throw new ClientValidationException($"{nameof(Client)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(client.PassportNumber))
                throw new ClientValidationException($"The {nameof(Client)} must have passport details.");

            try
            {
                _clientStorage.Add(client);

                var defaultAccount = new Account
                {
                    CurrencyName = "USD",
                    Amount = 0
                };

                _clientStorage.AddAccount(client.Id, defaultAccount);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while adding the {nameof(Client)}.", ex);
            }
        }

        public void AddAccount(Guid clientId, Account account)
        {
            try
            {
                _clientStorage.AddAccount(clientId, account);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while adding the account to the {nameof(Client)}.", ex);
            }
        }

        public void UpdateClient(Client client)
        {
            if (client == null)
                throw new ClientValidationException("The old or new client cannot be zero.");

            if (client.Age < 18)
                throw new ClientValidationException($"{nameof(Client)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(client.PassportNumber))
                throw new ClientValidationException($"The {nameof(Client)} must have passport details.");

            try
            {
                _clientStorage.Update(client);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while editing the {nameof(Client)}.", ex);
            }
        }

        public void UpdateAccount(Account account)
        {
            if (account.Amount < 0)
                throw new ClientValidationException("The new account balance cannot be negative.");

            try
            {
                _clientStorage.UpdateAccount(account);
            }
            catch (Exception ex)
            {
                throw new ClientException("An error occurred while editing the account.", ex);
            }
        }

        public ICollection<Client> Get(Expression<Func<Client, bool>> filter, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ClientException("Page number must be greater than zero.");

            if (pageSize <= 0)
                throw new ClientException("Page size must be greater than zero.");

            try
            {
                return _clientStorage.Get(filter, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while retrieving {nameof(Client)}.", ex);
            }
        }

        public Client GetById(Guid id)
        {
            try
            {
                return _clientStorage.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving {nameof(Client)}.", ex);
            }
        }

        public ICollection<Account> GetAccountsByClientId(Guid clientId)
        {
            try
            {
                return _clientStorage.GetAccountsByClientId(clientId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving {nameof(Account)}.", ex);
            }
        }

        public void DeleteClient(Guid clientId)
        {
            try
            {
                _clientStorage.Delete(clientId);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Client)}.", ex);
            }
        }

        public void DeleteAccount(Guid accountId)
        {
            try
            {
                _clientStorage.DeleteAccount(accountId);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Account)}.", ex);
            }
        }
    }
}
