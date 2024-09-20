using BankSystem.Domain.Models;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Пример обновления контракта сотрудника
            var employee = new Employee("Иван Иванов", "89991234567", new DateTime(1990, 5, 23));
            UpdateContract(employee);
            Console.WriteLine($"Обновленный контракт сотрудника: {employee.Contract}");

            // Пример обновления сущности валюты
            var currency = new Currency("USD", 100.50m);
            UpdateCurrency(ref currency);
            Console.WriteLine($"Обновленная валюта: Код - {currency.Code}, Сумма - {currency.Amount}");
        }

        /// <summary>
        /// Метод для обновления контракта сотрудника.
        /// Создает контракт на основе данных сотрудника.
        /// </summary>
        /// <param name="employee"></param>
        public static void UpdateContract(Employee employee)
        {
            employee.Contract = $"Контракт для {employee.FullName}, телефон: {employee.PhoneNumber}, возраст: {employee.Age} лет.";
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
