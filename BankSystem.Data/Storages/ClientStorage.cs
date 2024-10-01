using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage
    {
        private List<Client> _clients;

        public ClientStorage() 
        {
            _clients = new List<Client>();
        }

        public IEnumerable<Client> Clients => _clients;

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public Client? GetYoungestClient()
        {
            return _clients.OrderBy(c => c.Age)
                .FirstOrDefault();
        }

        public Client? GetOldestClient()
        {
            return _clients.OrderByDescending(c => c.Age)
                .FirstOrDefault();
        }

        public double GetAverageAgeClient()
        {
            return _clients.Average(c => c.Age);
        }
    }
}
