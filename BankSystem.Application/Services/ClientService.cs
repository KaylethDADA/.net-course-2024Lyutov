using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;

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

                var defaultAccount = GetDefaultAccount();
                _clientStorage.AddAccount(client, defaultAccount);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while adding the {nameof(Client)}.", ex);
            }
        }

        public void AddAccount(Client client, Account account)
        {
            try
            {
                _clientStorage.AddAccount(client, account);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while adding the account to the {nameof(Client)}.", ex);
            }
        }

        public void UpdateClient(Client newClient)
        {
            if (newClient == null)
                throw new ClientValidationException("The old or new client cannot be zero.");

            if (newClient.Age < 18)
                throw new ClientValidationException($"{nameof(Client)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(newClient.PassportNumber))
                throw new ClientValidationException($"The {nameof(Client)} must have passport details.");

            try
            {
                _clientStorage.Update(newClient);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while editing the {nameof(Client)}.", ex);
            }
        }

        public void UpdateAccount(Client client, Account oldAccount, Account newAccount)
        {
            if (oldAccount == null || newAccount == null)
                throw new ClientValidationException("The old or new personal account cannot be zero.");

            if (newAccount.Amount < 0)
                throw new ClientValidationException("The new account balance cannot be negative.");

            try
            {
                _clientStorage.UpdateAccount(client, oldAccount, newAccount);
            }
            catch (Exception ex)
            {
                throw new ClientException("An error occurred while editing the account.", ex);
            }
        }

        public List<Client> Get(Func<Client, bool>? filter)
        {
            return _clientStorage.Get(filter);
        }

        public void DeleteClient(Client client)
        {
            try
            {
                _clientStorage.Delete(client);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Client)}.", ex);
            }
        }

        public void DeleteAccount(Client client, Account account)
        {
            try
            {
                _clientStorage.DeleteAccount(client, account);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Client)}.", ex);
            }
        }

        private Account GetDefaultAccount()
        {
            return new Account
            {
                Currency = new Currency("USD", "$", "Доллар США"),
                Amount = 0
            };
        }
    }
}
