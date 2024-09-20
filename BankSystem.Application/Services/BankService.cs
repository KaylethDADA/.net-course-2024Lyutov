using BankSystem.Domain.Models;

namespace BankSystem.Application.Services
{
    public class BankService
    {
        /// <summary>
        /// Метод для расчета зарплаты владельцев банка.
        /// </summary>
        /// <param name="bankProfit">Прибыль банка.</param>
        /// <param name="bankExpenses">Расходы банка.</param>
        /// <param name="owners">Количество владельцев банка (сотрудников).</param>
        /// <returns>Зарплата владельца банка.</returns>
        public int CalculateOwnerSalary(int bankProfit, int bankExpenses, int ownersCount)
        {
            int salaryPerOwner = (bankProfit - bankExpenses) / ownersCount;
            
            return salaryPerOwner;
        }

        /// <summary>
        /// Метод для преобразования клиента в сотрудника.
        /// </summary>
        /// <param name="client">Клиент банка.</param>
        /// <returns>Новый сотрудник банка.</returns>
        public Employee ConvertClientToEmployee(Client client)
        {
            var newEmployee = new Employee(client.FullName, client.PhoneNumber, client.BirthDay)
            {
                Contract = $"Контракт для {client.FullName}\n Tелефон: {client.PhoneNumber}\n Возраст: {client.Age} лет."
            };

            return newEmployee;
        }
    }
}
