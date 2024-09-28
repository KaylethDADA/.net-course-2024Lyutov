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
            return clients.ToDictionary(c => c.PhoneNumber, c => c);
        }

        public Dictionary<Client, List<Account>> GenerateClientAccounts(List<Client> clients)
        {
            var random = new Random();
            var accountFaker = new Faker<Account>()
               .RuleFor(a => a.Currency, f => 
                    new Currency(
                        f.Finance.Currency().Code,
                        f.Finance.Currency().Description,
                        f.Finance.Currency().Symbol
                    ))
               .RuleFor(a => a.Amount, f => f.Finance.Amount(10, 10000));

            return clients.ToDictionary(
                c => c,
                c => Enumerable.Range(0, random.Next(1, 4))
                    .Select(_ => accountFaker.Generate())
                    .ToList()
            );
        }
    }
}
