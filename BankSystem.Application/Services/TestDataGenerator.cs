using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.Application.Services
{
    public class TestDataGenerator
    {
        public List<Client> GenerateClients(int count)
        {
            var clientFaker = new Faker<Client>()
                .CustomInstantiator(f => new Client(
                    f.Name.FullName(),
                    f.Phone.PhoneNumber(),
                    f.Date.Past(50, DateTime.Now.AddYears(-18))
                ));

            return clientFaker.Generate(count);
        }

        public List<Employee> GenerateEmployees(int count)
        {
            var employeeFaker = new Faker<Employee>()
                 .CustomInstantiator(f => new Employee(
                     f.Name.FullName(),
                     f.Phone.PhoneNumber(),
                     f.Date.Past(50, DateTime.Now.AddYears(-18)),
                     f.Commerce.Department(),
                     f.Random.Int(30000, 100000)
                 ));

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
                .CustomInstantiator(f => new Account(
                    new Currency(
                        f.Finance.Currency().Code,
                        f.Finance.Currency().Description,
                        f.Finance.Currency().Symbol),
                    f.Finance.Amount(10, 10000)
                ));

            return clients.ToDictionary(
                c => c,
                c => Enumerable.Range(0, random.Next(1, 4))
                    .Select(_ => accountFaker.Generate())
                    .ToList()
            );
        }
    }
}
