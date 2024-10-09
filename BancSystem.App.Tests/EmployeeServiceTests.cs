using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService _employeeService;
        private readonly EmployeeStorage _employeeStorage;
        private readonly TestDataGenerator _testDataGenerator;

        public EmployeeServiceTests()
        {
            _employeeStorage = new EmployeeStorage();
            _employeeService = new EmployeeService(_employeeStorage);
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

            var actualEmployees = _employeeService.Get(null);

            // Assert
            Assert.NotNull(actualEmployees);
            Assert.True(employees.SequenceEqual(actualEmployees));
        }

        [Fact]
        public void UpdateEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();
            _employeeService.Add(employee);

            var updatedEmployee = new Employee
            {
                PassportNumber = employee.PassportNumber,
                FullName = "Updated Name",
                BirthDay = employee.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
                Salary = 60000,
                Contract = "Updated Contract"
            };

            // Act
            _employeeService.Update(updatedEmployee);
            var actualEmployee = _employeeService.Get(e => e.PassportNumber == updatedEmployee.PassportNumber)
                .FirstOrDefault();

            // Assert
            Assert.NotNull(actualEmployee);
            Assert.Equal(updatedEmployee.FullName, actualEmployee.FullName);
            Assert.Equal(updatedEmployee.BirthDay, actualEmployee.BirthDay);
            Assert.Equal(updatedEmployee.PhoneNumber, actualEmployee.PhoneNumber);
            Assert.Equal(updatedEmployee.Salary, actualEmployee.Salary);
            Assert.Equal(updatedEmployee.Contract, actualEmployee.Contract);
        }

        [Fact]
        public void GetAllEmployeesPositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                _employeeService.Add(employee);
            }

            // Act
            var allEmployees = _employeeService.Get(null);

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(employees.Count, allEmployees.Count);
            Assert.True(employees.SequenceEqual(allEmployees));
        }

        [Fact]
        public void GetEmployeePositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                _employeeService.Add(employee);
            }
            var employeeToFind = employees.First();

            // Act
            var foundEmployee = _employeeService.Get(e => e.PassportNumber == employeeToFind.PassportNumber)
                .FirstOrDefault();

            // Assert
            Assert.NotNull(foundEmployee);
            Assert.Equal(employeeToFind, foundEmployee);
        }

        [Fact]
        public void DeleteEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();
            _employeeService.Add(employee);

            // Act
            _employeeService.Delete(employee);
            var allEmployees = _employeeService.Get(null);

            // Assert
            Assert.DoesNotContain(employee, allEmployees);
        }
    } 
}
