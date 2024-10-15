using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IStorage<Employee>
    {
        private readonly BankSystemDbContext _dbContext;

        public EmployeeStorage(BankSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Employee item)
        {
            var employy = _dbContext.Employees.FirstOrDefault(x => x.PassportNumber == item.PassportNumber);
            if (employy != null)
                throw new Exception($"A {nameof(Employee)} with the same passport number already exists.");

            _dbContext.Employees.Add(item);
            _dbContext.SaveChanges();
        }

        public void Update(Employee item)
        {
            var existingEmployee = _dbContext.Employees.Find(item.Id);

            if (existingEmployee == null)
                throw new Exception($"{nameof(Employee)} not found.");

            existingEmployee.FullName = item.FullName;
            existingEmployee.BirthDay = item.BirthDay;
            existingEmployee.PhoneNumber = item.PhoneNumber;
            existingEmployee.Salary = item.Salary;
            existingEmployee.Contract = item.Contract;

            _dbContext.SaveChanges();
        }

        public ICollection<Employee> Get(Expression<Func<Employee, bool>> filter, int pageNumber, int pageSize)
        {
            var employees = _dbContext.Employees.Where(filter).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return employees;
        }

        public Employee? GetById(Guid id)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.Id == id); 
        }

        public void Delete(Guid id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
                throw new Exception($"{nameof(Employee)} not found.");

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }
    }
}
