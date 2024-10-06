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

        public List<Employee> GetEmployeeByFilter(
            string? fullName,
            string? phoneNumber,
            string? passportNumber,
            DateTime? birthDateTo,
            DateTime? birthDateFrom)
        {
            var clients = _employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(fullName))
                clients = clients.Where(c => c.FullName.Contains(fullName));

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                clients = clients.Where(c => c.PhoneNumber.Contains(phoneNumber));

            if (birthDateFrom.HasValue)
                clients = clients.Where(c => c.BirthDay >= birthDateFrom.Value);

            if (birthDateTo.HasValue)
                clients = clients.Where(c => c.BirthDay <= birthDateTo.Value);

            return clients.ToList();
        }
    }
}
