using BankSystem.Application.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.Application.Services
{
    public class ClientService
    {
        private readonly ClientStorage _clientStorage;

        public ClientService(ClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddClient(Client client)
        {
            if (client.Age < 18)
                throw new ClientValidationException($"{nameof(Client)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(client.PassportNumber))
                throw new ClientValidationException($"The {nameof(Client)} must have passport details.");

            var existingClient = _clientStorage.GetAll().Keys
                .FirstOrDefault(c => c.PassportNumber == client.PassportNumber);

            if (existingClient != null)
                throw new ClientValidationException($"A {nameof(Client)} with the same passport number already exists.");

            try
            {
                _clientStorage.AddClient(client);

                var defaultAccount = GetDefaultAccount();
                _clientStorage.AddAccountToClient(client, defaultAccount);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while adding the {nameof(Client)}.", ex);
            }
        }

        public void AddAccountToClient(Client client, Account account)
        {
            if (_clientStorage.GetClient(client) is null)
                throw new ClientValidationException($"{nameof(Client)} not found.");

            try
            {
                _clientStorage.AddAccountToClient(client, account);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while adding the account to the {nameof(Client)}.", ex);
            }
        }

        public void EditClient(Client oldClient, Client newClient)
        {
            if (oldClient == null || newClient == null)
                throw new ClientValidationException("The old or new client cannot be zero.");

            if (newClient.Age < 18)
                throw new ClientValidationException($"{nameof(Client)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(newClient.PassportNumber))
                throw new ClientValidationException($"The {nameof(Client)} must have passport details.");

            try
            {
                _clientStorage.EditClient(oldClient, newClient);
            }
            catch (Exception ex)
            {
                throw new ClientException($"An error occurred while editing the {nameof(Client)}.", ex);
            }
        }

        public void EditAccount(Client client, Account oldAccount, Account newAccount)
        {
            if (oldAccount == null || newAccount == null)
                throw new ClientValidationException("The old or new personal account cannot be zero.");

            if (newAccount.Amount < 0)
                throw new ClientValidationException("The new account balance cannot be negative.");

            try
            {
                _clientStorage.EditAccount(client, oldAccount, newAccount);
            }
            catch (Exception ex)
            {
                throw new ClientException("An error occurred while editing the account.", ex);
            }
        }

        public Dictionary<Client, List<Account>> GetClientsByFilter(
            string? fullName,
            string? phoneNumber,
            string? passportNumber,
            DateTime? birthDateTo,
            DateTime? birthDateFrom)
        {
            return _clientStorage.GetClientsByFilter(fullName, phoneNumber, passportNumber, birthDateTo, birthDateFrom);
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
