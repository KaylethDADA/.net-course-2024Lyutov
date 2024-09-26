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
            var newEmployee = new Employee(client.FullName, client.PhoneNumber, client.BirthDay)
            {
                Contract = $"Новый контракт для нового сотрудника: {client.FullName}\n Tелефон: {client.PhoneNumber}\n Возраст: {client.Age} лет."
            };

            return newEmployee;
        }
    }
}
