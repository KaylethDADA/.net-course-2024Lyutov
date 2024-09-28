using BankSystem.Domain.Models;

namespace BankSystem.Application.Services
{
    public class BankService
    {
        public int CalculateOwnerSalary(int bankProfit, int bankExpenses, int ownersCount)
        {
            int salaryPerOwner = (bankProfit - bankExpenses) / ownersCount;
            
            return salaryPerOwner;
        }

        public Employee ConvertClientToEmployee(Client client)
        {
            return new Employee
            {
                FullName = client.FullName,
                Contract = $"Новый контракт для нового сотрудника: {client.FullName}\n Tелефон: {client.PhoneNumber}\n Возраст: {client.Age} лет.",
                BirthDay = client.BirthDay,
                PhoneNumber = client.PhoneNumber,
                Salary = 10000
            };
        }
    }
}
