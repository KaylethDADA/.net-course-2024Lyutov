using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        private Dictionary<Client, List<Account>> _clientAccounts;

        public ClientStorage()
        {
            _clientAccounts = new Dictionary<Client, List<Account>>();
        }

        public void Add(Client item)
        {
            if(_clientAccounts.Keys.Any(x => x.PassportNumber == item.PassportNumber))
                throw new Exception($"A {nameof(Client)} with the same passport number already exists.");

            _clientAccounts[item] = new List<Account>();
        }

        public void AddAccount(Client client, Account account)
        {
            if (!_clientAccounts.ContainsKey(client))
                throw new Exception($"{nameof(Client)} not found.");

            _clientAccounts[client].Add(account);
        }

        public void Update(Client item)
        {
            var client = _clientAccounts.Keys.Where(x => x.Equals(item)).First();

            if (client == null)
                throw new Exception($"{nameof(Client)} not found.");

            client.FullName = item.FullName;
            client.PhoneNumber = item.PhoneNumber;
            client.BirthDay = item.BirthDay;
        }

        public void UpdateAccount(Client client, Account oldAccount, Account newAccount)
        {
            if (!_clientAccounts.TryGetValue(client, out var accounts))
                throw new Exception($"{nameof(Client)} not found.");

            var index = accounts.IndexOf(oldAccount);

            if (index == -1)
                throw new Exception($"{nameof(Account)} not found.");

            accounts[index] = newAccount;
        }

        public Dictionary<Client, List<Account>> Get(Func<Client, bool>? filter)
        {
            var clients = _clientAccounts.AsEnumerable();

            if (filter != null)
                clients = clients.Where(x => filter(x.Key));

            return clients.ToDictionary(x => x.Key, x => x.Value);
        }

        public void Delete(Client item)
        {
            if (!_clientAccounts.ContainsKey(item))
                throw new Exception($"{nameof(Client)} not found.");

            _clientAccounts.Remove(item);
        }

        public void DeleteAccount(Client client, Account account)
        {
            if (!_clientAccounts.TryGetValue(client, out var accounts))
                throw new Exception($"{nameof(Client)} not found.");

            accounts.Remove(account);
        }

        public List<Account> GetClientAccounts(Client client)
        {   
            if (!_clientAccounts.TryGetValue(client, out var accounts))
                throw new Exception($"{nameof(Client)} not found.");

            return accounts;
        }
    }
}
