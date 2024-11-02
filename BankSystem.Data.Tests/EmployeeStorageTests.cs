using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Tests
{
   /* public class EmployeeStorageTests
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly EmployeeStorage _employeeStorage;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly CancellationToken _token;

        public EmployeeStorageTests()
        {
            _dbContext = new BankSystemDbContext();
            _employeeStorage = new EmployeeStorage(_dbContext);
            _testDataGenerator = new TestDataGenerator();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task AddEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();

            // Act
            await _employeeStorage.AddAsync(employee, _token);

            // Assert
            var actualEmployees = await _employeeStorage.GetByIdAsync(employee.Id, _token);

            Assert.NotNull(actualEmployees);
            Assert.Equal(employee, actualEmployees);
        }

        [Fact]
        public async Task UpdateEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();

            await _employeeStorage.AddAsync(employee, _token);

            // Create updated employee
            var updatedEmployee = new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PassportNumber = employee.PassportNumber,
                BirthDay = employee.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
                Salary = 60000,
                Contract = "UpContract"
            };

            // Act
            await _employeeStorage.UpdateAsync(updatedEmployee, _token);
            var actualEmployee = await _employeeStorage.GetByIdAsync(updatedEmployee.Id, _token);

            // Assert
            Assert.NotNull(actualEmployee);
            Assert.Equal(updatedEmployee.BirthDay, actualEmployee.BirthDay);
            Assert.Equal(updatedEmployee.PhoneNumber, actualEmployee.PhoneNumber);
            Assert.Equal(updatedEmployee.Salary, actualEmployee.Salary);
            Assert.Equal(updatedEmployee.Contract, actualEmployee.Contract);
        }

        [Fact]
        public async Task GetByIdEmployeePositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                await _employeeStorage.AddAsync(employee, _token);
            }
            var employeeToFind = employees.First();

            // Act
            var foundEmployee = await _employeeStorage.GetByIdAsync(employeeToFind.Id, _token);

            // Assert
            Assert.NotNull(foundEmployee);
            Assert.Equal(employeeToFind, foundEmployee);
        }

        [Fact]
        public async Task GetPageEmployeesPositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                await _employeeStorage.AddAsync(employee, _token);
            }

            // Act
            var allEmployees = await _employeeStorage.GetAsync(e => true, 1, 10, _token);

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(10, allEmployees.Count);
        }

        [Fact]
        public async Task DeleteEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();
            await _employeeStorage.AddAsync(employee, _token);

            // Act
            await _employeeStorage.DeleteAsync(employee.Id, _token);

            // Assert
            var deletedEmployees = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == employee.Id);
            Assert.Null(deletedEmployees);
        }
    }*/
}
