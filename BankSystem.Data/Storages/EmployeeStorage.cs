using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage
    {
        private List<Employee> _employees;

        public EmployeeStorage()
        {
            _employees = new List<Employee>();
        }

        public IEnumerable<Employee> Employees => _employees;


        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        public Employee? GetYoungestEmployee()
        {
            return _employees.OrderBy(c => c.Age)
                .FirstOrDefault();
        }

        public Employee? GetOldestEmployee()
        {
            return _employees.OrderByDescending(c => c.Age)
                .FirstOrDefault();
        }

        public double GetAverageAgeEmployee()
        {
            return _employees.Average(c => c.Age);
        }
    }
}
