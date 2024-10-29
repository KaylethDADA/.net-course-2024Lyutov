using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(Employee item, CancellationToken cancellationToken)
        {
            var employy = await _dbContext.Employees.FirstOrDefaultAsync(x => x.PassportNumber == item.PassportNumber);
            if (employy != null)
                throw new Exception($"A {nameof(Employee)} with the same passport number already exists.");

            await _dbContext.Employees.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee item, CancellationToken cancellationToken)
        {
            var existingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (existingEmployee == null)
                throw new Exception($"{nameof(Employee)} not found.");

            existingEmployee.FirstName = item.FirstName;
            existingEmployee.LastName = item.LastName;
            existingEmployee.BirthDay = item.BirthDay;
            existingEmployee.PhoneNumber = item.PhoneNumber;
            existingEmployee.Salary = item.Salary;
            existingEmployee.Contract = item.Contract;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Employee>> GetAsync(
            Expression<Func<Employee, bool>>? filter,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            var quer = _dbContext.Employees.AsQueryable();

            if (filter != null)
                quer = quer.Where(filter);

            if (pageNumber != null && pageSize != null)
                quer = quer.Skip((pageNumber.Value - 1) * pageSize.Value)
                           .Take(pageSize.Value);

            return await quer.ToListAsync();
        }

        public async Task<Employee>? GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
                throw new Exception($"{nameof(Employee)} not found.");

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
