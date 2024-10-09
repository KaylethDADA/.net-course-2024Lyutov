using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IEmployeeStorage
    {
        private List<Employee> _employees;

        public EmployeeStorage()
        {
            _employees = new List<Employee>();
        }

        public void Add(Employee item)
        {
            if (_employees.Any(x => x.PassportNumber == item.PassportNumber))
                throw new Exception($"A {nameof(Employee)} with the same passport number already exists.");

            _employees.Add(item);
        }

        public void Update(Employee item)
        {
            var existingEmployee = _employees.FirstOrDefault(x => x.Equals(item));
            
            if (existingEmployee == null)
                throw new Exception($"{nameof(Employee)} not found.");

            existingEmployee.FullName = item.FullName;
            existingEmployee.BirthDay = item.BirthDay;
            existingEmployee.PhoneNumber = item.PhoneNumber;
            existingEmployee.Salary = item.Salary;
            existingEmployee.Contract = item.Contract;
        }

        public List<Employee> Get(Func<Employee, bool>? filter)
        {
            var employees = _employees.AsEnumerable();

            if (filter != null)
                employees = employees.Where(filter);

            return employees.ToList();
        }

        public void Delete(Employee item)
        {
            if (!_employees.Contains(item))
                throw new Exception($"{nameof(Employee)} not found.");

            _employees.Remove(item);
        }
    }
}
