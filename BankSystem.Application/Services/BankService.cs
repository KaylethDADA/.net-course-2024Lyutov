using BankSystem.Domain.Models;

namespace BankSystem.Application.Services
{
    public class BankService
    {
        private readonly List<Person> _blackList;
        private readonly Dictionary<Person, decimal> _bonuses;

        public BankService()
        {
            _blackList = new List<Person>();
            _bonuses = new Dictionary<Person, decimal>();
        }

        public void AddBonus<TType>(TType person, decimal bonusAmount)
            where TType : Person
        {
            if (!_bonuses.ContainsKey(person))
                _bonuses[person] = 0;

            _bonuses[person] += bonusAmount;
        }

        public void AddToBlackList<TType>(TType person)
            where TType : Person
        {
            if (!_blackList.Contains(person))
            {
                _blackList.Add(person);
            }
        }

        public bool IsPersonInBlackList<TType>(TType person) 
            where TType : Person
        {
            return _blackList.Contains(person);
        }

        public decimal GetBonus(Client client)
        {
            return _bonuses.ContainsKey(client) ? _bonuses[client] : 0;
        }

        public int CalculateOwnerSalary(int bankProfit, int bankExpenses, int ownersCount)
        {
            int salaryPerOwner = (bankProfit - bankExpenses) / ownersCount;
            
            return salaryPerOwner;
        }

        public Employee ConvertClientToEmployee(Client client, string contract, int salary)
        {
            return new Employee
            { 
                FullName = client.FullName,
                PhoneNumber = client.PhoneNumber,
                BirthDay = client.BirthDay,
                Contract = contract,
                Salary = salary };
        }
    }
}
