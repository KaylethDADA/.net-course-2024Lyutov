using BankSystem.Application.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
   /* public class EmployeeServiceTests
    {
        private readonly EmployeeService _employeeService;
        private readonly TestDataGenerator _testDataGenerator;
        private readonly CancellationToken _token;

        public EmployeeServiceTests()
        {
            var dbContext = new BankSystemDbContext();
            _employeeService = new EmployeeService(new EmployeeStorage(dbContext));
            _testDataGenerator = new TestDataGenerator();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task AddEmployeePositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);

            // Act
            foreach (var employee in employees)
            {
                await _employeeService.AddEmploeeAsync(employee, _token);
            }

            var actualEmployees = await _employeeService.GetAsync(e => true, 1, 10, _token);

            // Assert
            Assert.NotNull(actualEmployees);
            Assert.Equal(employees.Count, actualEmployees.Count);
        }

        [Fact]
        public async Task UpdateEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();
            await _employeeService.AddEmploeeAsync(employee, _token);

            var updatedEmployee = new Employee
            {
                Id = employee.Id,
                FirstName = "ad",
                LastName = "adad",
                PassportNumber = employee.PassportNumber,
                BirthDay = employee.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
                Salary = 60000,
                Contract = "UpContract"
            };

            // Act
            await _employeeService.UpdateAsync(updatedEmployee, _token);
            var actualEmployee = await _employeeService.GetByIdAsync(updatedEmployee.Id, _token);

            // Assert
            Assert.NotNull(actualEmployee);
            Assert.Equal(updatedEmployee.BirthDay, actualEmployee.BirthDay);
            Assert.Equal(updatedEmployee.PhoneNumber, actualEmployee.PhoneNumber);
            Assert.Equal(updatedEmployee.Salary, actualEmployee.Salary);
            Assert.Equal(updatedEmployee.Contract, actualEmployee.Contract);
        }

        [Fact]
        public async Task GetFilterEmployeesPositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                await _employeeService.AddEmploeeAsync(employee, _token);
            }

            // Act
            var allEmployees = await _employeeService.GetAsync(e => true, 1, 10, _token);

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(employees.Count, allEmployees.Count);
        }

        [Fact]
        public async Task GetByIdEmployeePositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
               await  _employeeService.AddEmploeeAsync(employee, _token);
            }
            var employeeToFind = employees.First();

            // Act
            var foundEmployee = await _employeeService.GetByIdAsync(employeeToFind.Id, _token);

            // Assert
            Assert.NotNull(foundEmployee);
            Assert.Equal(employeeToFind.PassportNumber, foundEmployee.PassportNumber);
        }

        [Fact]
        public async Task DeleteEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();
            await _employeeService.AddEmploeeAsync(employee, _token);

            // Act
            await _employeeService.DeleteAsync(employee.Id, _token);

            // Assert
            var exEmployees = await _employeeService.GetByIdAsync(employee.Id, _token);
            Assert.Null(exEmployees);
        }
    } */
}
