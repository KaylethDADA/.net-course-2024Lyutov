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

        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        public void UpdateEmployee(Employee oldEmployee, Employee newEmployee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Equals(oldEmployee));
            
            if (existingEmployee == null)
                throw new Exception($"{nameof(Employee)} not found.");

            existingEmployee.FullName = newEmployee.FullName;
            existingEmployee.BirthDay = newEmployee.BirthDay;
            existingEmployee.PhoneNumber = newEmployee.PhoneNumber;
            existingEmployee.Salary = newEmployee.Salary;
            existingEmployee.Contract = newEmployee.Contract;
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee? GetEmployee(Employee employee)
        {
            return _employees.FirstOrDefault(x => x.Equals(employee));
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
