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
