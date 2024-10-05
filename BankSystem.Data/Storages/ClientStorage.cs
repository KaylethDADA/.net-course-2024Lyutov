using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage
    {
        private Dictionary<Client, List<Account>> _clientAccounts;

        public ClientStorage()
        {
            _clientAccounts = new Dictionary<Client, List<Account>>();
        }

        public void AddClient(Client client)
        {
            _clientAccounts[client] = new List<Account>();
        }

        public void AddAccountToClient(Client client, Account account)
        {
            _clientAccounts[client].Add(account);
        }

        public void EditAccount(Client client, Account oldAccount, Account newAccount)
        {
            if (!_clientAccounts.TryGetValue(client, out var accounts))
                throw new Exception($"{nameof(Client)} not found.");
            
            var index = accounts.IndexOf(oldAccount);

            if (index == -1)
                throw new Exception($"{nameof(Account)} not found.");
            
            accounts[index] = newAccount;
        }

        public void EditClient(Client oldClient, Client newClient)
        {
            if (!_clientAccounts.ContainsKey(oldClient))
                throw new Exception($"{nameof(Client)} not found.");

            var accounts = _clientAccounts[oldClient];
            _clientAccounts.Remove(oldClient);
            _clientAccounts[newClient] = accounts;
        }

        public Client? GetClient(Client client)
        {
            if (!_clientAccounts.Keys.Contains(client))
                return null;

            return client;
        }

        public List<Account> GetClientAccounts(Client client)
        {
            if (!_clientAccounts.TryGetValue(client, out var accounts))
                throw new Exception($"{nameof(Client)} not found.");

            return accounts;
        }

        public Client? GetYoungestClient()
        {
            return _clientAccounts.Keys.OrderBy(c => c.Age)
                .FirstOrDefault();
        }

        public Client? GetOldestClient()
        {
            return _clientAccounts.Keys.OrderByDescending(c => c.Age)
                .FirstOrDefault();
        }

        public double GetAverageAgeClient()
        {
            return _clientAccounts.Keys.Average(c => c.Age);
        }

        public Dictionary<Client, List<Account>> GetAll()
        {
            return _clientAccounts;
        }
    }
}
