using BankSystem.Application.Services;
using BankSystem.Domain.Models;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание к теме "Типы значений и ссылочные типы".
            Zadanie_1();

            // Задание к теме "Приведение и преобразование типов".
            Zadanie_2();
        }

        /// <summary>
        /// Задание к теме "Типы значений и ссылочные типы".
        /// </summary>
        public static void Zadanie_1()
        {
            // Пример обновления контракта сотрудника
            var employee = new Employee("Иван Иванов", "89991234567", new DateTime(1990, 5, 23));
            UpdateContract(employee);

            // Пример обновления сущности валюты
            var currency = new Currency("USD", 100.50m);
            UpdateCurrency(ref currency);
        }

        /// <summary>
        /// Задание к теме "Приведение и преобразование типов".
        /// </summary>
        public static void Zadanie_2()
        {
            var bankService = new BankService();

            // Пример расчета зарплаты владельцев банка
            int ownersCount = 3;
            int bankProfit = 1000000;
            int bankExpenses = 500000;
            int salary = bankService.CalculateOwnerSalary(bankProfit, bankExpenses, ownersCount);

            // Пример преобразования клиента в сотрудника
            var client = new Client("Алексей Алексеев", "111222", new DateTime(2000, 3, 3));
            var newEmployee = bankService.ConvertClientToEmployee(client);
        }

        /// <summary>
        /// Метод для обновления контракта сотрудника.
        /// Создает контракт на основе данных сотрудника.
        /// </summary>
        /// <param name="employee"></param>
        public static void UpdateContract(Employee employee)
        {
            employee.Contract = $"Контракт для {employee.FullName}\n Tелефон: {employee.PhoneNumber}\n Возраст: {employee.Age} лет.";
        }

        /// <summary>
        /// Метод для обновления сущности валюты.
        /// Поскольку структура передается по значению, используем ref.
        /// </summary>
        /// <param name="currency"></param>
        public static void UpdateCurrency(ref Currency currency)
        {
            currency = new Currency(currency.Code, currency.Amount + 50);
        }
    }
}
