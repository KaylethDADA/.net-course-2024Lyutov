using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.Application.Services
{
    public class TestDataGenerator
    {
        public List<Client> GenerateClients(int count)
        {
            var clientFaker = new Faker<Client>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FullName, f => new FullName
                {
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    MiddleName = f.Random.Bool() ? f.Name.LastName() + f.Name.FirstName() : null
                })
                .RuleFor(c => c.PhoneNumber, f => $"+373 77 {f.Random.Int(4, 9)} {f.Random.Number(100, 999)}")
                .RuleFor(c => c.PassportNumber, f => f.Random.String2(10, "0123456789"))
                .RuleFor(c => c.BirthDay, f => f.Date.Past(50, DateTime.Now.AddYears(-18)))
                .RuleFor(c => c.Accounts, _ => new List<Account>());

            return clientFaker.Generate(count);
        }

        public List<Employee> GenerateEmployees(int count)
        {
            var employeeFaker = new Faker<Employee>()
                .RuleFor(e => e.Id, f => Guid.NewGuid())
                .RuleFor(e => e.FullName, f => new FullName
                {
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    MiddleName = f.Random.Bool() ? f.Name.LastName() + f.Name.FirstName() : null
                })
                .RuleFor(e => e.PhoneNumber, f => $"+373 77 {f.Random.Int(4, 9)} {f.Random.Number(100, 999)}")
                .RuleFor(e => e.PassportNumber, f => f.Random.String2(10, "0123456789"))
                .RuleFor(e => e.BirthDay, f => f.Date.Past(50, DateTime.Now.AddYears(-18)))
                .RuleFor(e => e.Salary, f => f.Random.Int(30000, 100000))
                .RuleFor(e => e.Contract, f => f.Random.Bool() ? "Permanent" : "Temporary");

            return employeeFaker.Generate(count);
        }

        public List<Account> GenerateAccounts(int count, List<Currency> currencies)
        {
            var accountFaker = new Faker<Account>()
                .RuleFor(a => a.Id, f => Guid.NewGuid())
                .RuleFor(a => a.Currency, f => f.PickRandom(currencies))
                .RuleFor(a => a.CurrencyId, (f, a) => a.Currency.Id)
                .RuleFor(a => a.Amount, f => f.Finance.Amount(10, 10000))
                .RuleFor(a => a.Client, _ => null)
                .RuleFor(a => a.ClientId, _ => Guid.Empty);

            return accountFaker.Generate(count);
        }

        public List<Currency> GenerateCurrencies(int count)
        {
            var currencyFaker = new Faker<Currency>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Code, f => f.Finance.Currency().Code)
                .RuleFor(c => c.Symbol, f => f.Finance.Currency().Symbol)
                .RuleFor(c => c.Description, f => f.Finance.Currency().Description);

            return currencyFaker.Generate(count);
        }

        public Dictionary<string, Client> GenerateClientDictionary(List<Client> clients)
        {
            return clients.ToDictionary(c => c.PhoneNumber, c => c);
        }

        public Dictionary<Client, List<Account>> GenerateClientAccounts(List<Client> clients, List<Currency> currencies)
        {
            var random = new Random();
            var result = new Dictionary<Client, List<Account>>();

            foreach (var client in clients)
            {
                var accounts = GenerateAccounts(random.Next(1, 4), currencies);
                accounts.ForEach(a =>
                {
                    a.ClientId = client.Id;
                    a.Client = client;
                });

                result[client] = accounts;
            }

            return result;
        }
    }
}
