using BankSystem.Application.Services;
using BankSystem.Data.Storages;

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
            var employee = _testDataGenerator.GenerateEmployees(1).First();

            // Act
            _employeeService.AddEmployee(employee);

            // Assert
            var storedEmployees = _employeeStorage.GetEmployee(employee);
            Assert.Equal(employee, storedEmployees);
        }


        [Fact]
        public void UpdateEmployeePositiveTest()
        {
            // Arrange
            var oldEmployee = _testDataGenerator.GenerateEmployees(1).First();
            var newEmployee = _testDataGenerator.GenerateEmployees(1).First();
            newEmployee.PassportNumber = oldEmployee.PassportNumber;

            _employeeStorage.AddEmployee(oldEmployee);

            // Act
            _employeeService.UpdateEmployee(oldEmployee, newEmployee);

            // Assert
            var storedEmployees = _employeeStorage.GetAllEmployees();
            Assert.Contains(newEmployee, storedEmployees);
        }


        [Fact]
        public void GetEmployeeByFilterPositiveTest()
        {
            // Arrange
            var employee1 = _testDataGenerator.GenerateEmployees(1).First();
            var employee2 = _testDataGenerator.GenerateEmployees(1).First();

            _employeeStorage.AddEmployee(employee1);
            _employeeStorage.AddEmployee(employee2);

            // Act
            var result = _employeeService.GetEmployeeByFilter(employee1.FullName, null, null, null, null);

            // Assert
            Assert.Single(result);
            Assert.Contains(employee1, result);
        }
    } 
}
