using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.Application.Services
{
    public class TestDataGenerator
    {
        public List<Client> GenerateClients(int count)
        {
            var clients = new List<Client>();

            var clientFaker = new Faker<Client>()
                .RuleFor(c => c.FullName, f => f.Name.FullName())
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.BirthDay, f => f.Date.Past(50, DateTime.Now.AddYears(-18)));

            return clientFaker.Generate(count);
        }

        public List<Employee> GenerateEmployees(int count)
        {
            var employeeFaker = new Faker<Employee>()
                .RuleFor(e => e.FullName, f => f.Name.FullName())
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.BirthDay, f => f.Date.Past(50, DateTime.Now.AddYears(-18)))
                .RuleFor(e => e.Salary, f => f.Random.Int(30000, 100000));

            return employeeFaker.Generate(count);
        }

        public Dictionary<string, Client> GenerateClientDictionary(List<Client> clients)
        {
            var clientDictionary = new Dictionary<string, Client>();
            foreach (var client in clients)
            {
                clientDictionary[client.PhoneNumber] = client;
            }
            return clientDictionary;
        }
    }
}
