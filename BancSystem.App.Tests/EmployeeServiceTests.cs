using BankSystem.Application.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService _employeeService;
        private readonly TestDataGenerator _testDataGenerator;

        public EmployeeServiceTests()
        {
            var dbContext = new BankSystemDbContext();
            _employeeService = new EmployeeService(new EmployeeStorage(dbContext));
            _testDataGenerator = new TestDataGenerator();
        }

        [Fact]
        public void AddEmployeePositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);

            // Act
            foreach (var employee in employees)
            {
                _employeeService.Add(employee);
            }

            var actualEmployees = _employeeService.Get(e => true, 1, 10);

            // Assert
            Assert.NotNull(actualEmployees);
            Assert.Equal(employees.Count, actualEmployees.Count);
        }

        [Fact]
        public void UpdateEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();
            _employeeService.Add(employee);

            var updatedEmployee = new Employee
            {
                Id = employee.Id,
                PassportNumber = employee.PassportNumber,
                FullName = new FullName { FirstName = "UpName", LastName = "UpLName" },
                BirthDay = employee.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
                Salary = 60000,
                Contract = "UpContract"
            };

            // Act
            _employeeService.Update(updatedEmployee);
            var actualEmployee = _employeeService.GetById(updatedEmployee.Id);

            // Assert
            Assert.NotNull(actualEmployee);
            Assert.Equal(updatedEmployee.FullName.FirstName, actualEmployee.FullName.FirstName);
            Assert.Equal(updatedEmployee.FullName.LastName, actualEmployee.FullName.LastName);
            Assert.Equal(updatedEmployee.BirthDay, actualEmployee.BirthDay);
            Assert.Equal(updatedEmployee.PhoneNumber, actualEmployee.PhoneNumber);
            Assert.Equal(updatedEmployee.Salary, actualEmployee.Salary);
            Assert.Equal(updatedEmployee.Contract, actualEmployee.Contract);
        }

        [Fact]
        public void GetFilterEmployeesPositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                _employeeService.Add(employee);
            }

            // Act
            var allEmployees = _employeeService.Get(e => true, 1, 10);

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(employees.Count, allEmployees.Count);
        }

        [Fact]
        public void GetByIdEmployeePositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                _employeeService.Add(employee);
            }
            var employeeToFind = employees.First();

            // Act
            var foundEmployee = _employeeService.GetById(employeeToFind.Id);

            // Assert
            Assert.NotNull(foundEmployee);
            Assert.Equal(employeeToFind.PassportNumber, foundEmployee.PassportNumber);
        }

        [Fact]
        public void DeleteEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();
            _employeeService.Add(employee);

            // Act
            _employeeService.Delete(employee.Id);
            var allEmployees = _employeeService.Get(e => true, 1, 10); 

            // Assert
            Assert.DoesNotContain(allEmployees, e => e.Id == employee.Id);
        }
    } 
}
