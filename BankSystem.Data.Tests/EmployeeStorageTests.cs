using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Tests
{
    public class EmployeeStorageTests
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly EmployeeStorage _employeeStorage;
        private readonly TestDataGenerator _testDataGenerator;

        public EmployeeStorageTests()
        {
            _dbContext = new BankSystemDbContext();
            _employeeStorage = new EmployeeStorage(_dbContext);
            _testDataGenerator = new TestDataGenerator();
        }

        [Fact]
        public void AddEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();

            // Act
            _employeeStorage.Add(employee);

            // Assert
            var actualEmployees = _employeeStorage.GetById(employee.Id);

            Assert.NotNull(actualEmployees);
            Assert.Equal(employee, actualEmployees);
        }

        [Fact]
        public void UpdateEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();

            _employeeStorage.Add(employee);

            // Create updated employee
            var updatedEmployee = new Employee
            {
                Id = employee.Id,
                PassportNumber = employee.PassportNumber,
                FullName = new FullName { FirstName = "UpName", LastName = "UpLName"},
                BirthDay = employee.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
                Salary = 60000,
                Contract = "UpContract"
            };

            // Act
            _employeeStorage.Update(updatedEmployee);
            var actualEmployee = _employeeStorage.GetById(updatedEmployee.Id);

            // Assert
            Assert.NotNull(actualEmployee);
            Assert.Equal(updatedEmployee.FullName, actualEmployee.FullName);
            Assert.Equal(updatedEmployee.BirthDay, actualEmployee.BirthDay);
            Assert.Equal(updatedEmployee.PhoneNumber, actualEmployee.PhoneNumber);
            Assert.Equal(updatedEmployee.Salary, actualEmployee.Salary);
            Assert.Equal(updatedEmployee.Contract, actualEmployee.Contract);
        }

        [Fact]
        public void GetByIdEmployeePositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                _employeeStorage.Add(employee);
            }
            var employeeToFind = employees.First();

            // Act
            var foundEmployee = _employeeStorage.GetById(employeeToFind.Id);

            // Assert
            Assert.NotNull(foundEmployee);
            Assert.Equal(employeeToFind, foundEmployee);
        }

        [Fact]
        public void GetPageEmployeesPositiveTest()
        {
            // Arrange
            var employees = _testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                _employeeStorage.Add(employee);
            }

            // Act
            var allEmployees = _employeeStorage.Get(e => true, 1, 10);

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(10, allEmployees.Count);
        }

        [Fact]
        public void DeleteEmployeePositiveTest()
        {
            // Arrange
            var employee = _testDataGenerator.GenerateEmployees(1).First();
            _employeeStorage.Add(employee);

            // Act
            _employeeStorage.Delete(employee.Id);

            // Assert
            var deletedEmployees = _dbContext.Clients.FirstOrDefault(c => c.Id == employee.Id);
            Assert.Null(deletedEmployees);
        }
    }
}
