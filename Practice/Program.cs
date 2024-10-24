using BankSystem.Application.Services;
using BankSystem.Domain.Models;
using System.Diagnostics;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Задание к теме List, Dictionary
            var stopwatch = new Stopwatch();
            var testDataGenerator = new TestDataGenerator();

            var clients = testDataGenerator.GenerateClients(1000);
            var employees = testDataGenerator.GenerateEmployees(1000);
            var clientDictionary = testDataGenerator.GenerateClientDictionary(clients);

            string phoneNumberToSearch = clients[999].PhoneNumber;

            // Замер времени поиска клиента по номеру телефона в списке
            stopwatch.Start();
            var clientFromList = clients.FirstOrDefault(c => c.PhoneNumber == phoneNumberToSearch);
            stopwatch.Stop();
            Console.WriteLine($"Поиск в списке занял: {stopwatch.Elapsed.TotalMilliseconds} ms");

            // Замер времени поиска клиента по номеру телефона в словаре
            stopwatch.Restart();
            var clientFromDictionary = clientDictionary[phoneNumberToSearch];
            stopwatch.Stop();
            Console.WriteLine($"Поиск в словаре занял: {stopwatch.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine();

            // Выборка клиентов по возрасту
            int ageLimit = 30;
            stopwatch.Restart();
            var youngClients = clients.Where(c => c.Age < ageLimit).ToList();
            stopwatch.Stop();
            Console.WriteLine($"Найдено клиентов моложе {ageLimit} лет: {youngClients.Count}, за {stopwatch.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine();

            // Поиск сотрудника с минимальной зарплатой
            stopwatch.Restart();
            var employeeWithMinSalary = employees.OrderBy(e => e.Salary).FirstOrDefault();
            stopwatch.Stop();
            Console.WriteLine($"Сотрудник с минимальной зарплатой: {employeeWithMinSalary?.FullName}, Зарплата: {employeeWithMinSalary?.Salary}, за {stopwatch.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine();

            // Сравнение поиска по словарю двумя методами
            // 1. Поиск последнего элемента с FirstOrDefault
            stopwatch.Restart();
            var lastClientUsingFirstOrDefault = clientDictionary.Values.FirstOrDefault(c => c.PhoneNumber == phoneNumberToSearch);
            stopwatch.Stop();
            Console.WriteLine($"Поиск с FirstOrDefault занял: {stopwatch.Elapsed.TotalMilliseconds} ms");

            // 2. Поиск последнего элемента по ключу
            stopwatch.Restart();
            var lastClientByKey = clientDictionary[phoneNumberToSearch];
            stopwatch.Stop();
            Console.WriteLine($"Поиск по ключу занял: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }

        //public static void Practices()
        //{
        //    //Задание к теме "Типы значений и ссылочные типы".
        //    var employee = new Employee
        //    {
        //        FullName = "Иван Иванов",
        //        PhoneNumber = "89991234567",
        //        BirthDay = new DateTime(1990, 5, 23),
        //        Contract = "",
        //        Salary = 1
        //    };
        //    UpdateContract(employee);

        //    var currency = new Currency("USD","","");
        //    UpdateCurrency(ref currency);


        //    //Задание к теме "Приведение и преобразование типов".
        //    var bankService = new BankService();

        //    int ownersCount = 3;
        //    int bankProfit = 1000000;
        //    int bankExpenses = 500000;
        //    int salary = bankService.CalculateOwnerSalary(bankProfit, bankExpenses, ownersCount);

        //    var client = new Client
        //    {
        //        FullName = "Алексей Алексеев",
        //        PhoneNumber = "111222",
        //        BirthDay = new DateTime(2000, 3, 3)
        //    };
        //    var newEmployee = bankService.ConvertClientToEmployee(client, "", 1);
        //}

        //public static void UpdateContract(Employee employee)
        //{
        //    employee.Contract = $"Контракт для {employee.FullName}\n Tелефон: {employee.PhoneNumber}\n Возраст: {employee.Age} лет.";
        //}

        //public static void UpdateCurrency(ref Currency currency)
        //{
        //    currency = new Currency(currency.Code, "", "");
        //}
    }
}
