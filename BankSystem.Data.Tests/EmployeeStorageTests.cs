using BankSystem.Application.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Tests
{
    public class EmployeeStorageTests
    {
        [Fact]
        public void AddEmployeePositiveTest()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();

            var employees = testDataGenerator.GenerateEmployees(10);

            // Act
            foreach (var employee in employees)
            {
                storage.Add(employee);
            }

            var actualEmployees = storage.Get(null);

            // Assert
            Assert.NotNull(actualEmployees);
            Assert.True(employees.SequenceEqual(actualEmployees));
        }

        [Fact]
        public void UpdateEmployeePositiveTest()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();
            var employee = testDataGenerator.GenerateEmployees(1).First();
            storage.Add(employee);

            // Create updated employee
            var updatedEmployee = new Employee
            {
                PassportNumber = employee.PassportNumber, // same identifier
                FullName = "Updated Name",
                BirthDay = employee.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
                Salary = 60000,
                Contract = "Updated Contract"
            };

            // Act
            storage.Update(updatedEmployee);
            var actualEmployee = storage.Get(e => e.PassportNumber == employee.PassportNumber).FirstOrDefault();

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
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();
            var employees = testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                storage.Add(employee);
            }

            // Act
            var allEmployees = storage.Get(null);

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(employees.Count, allEmployees.Count);
            Assert.True(employees.SequenceEqual(allEmployees));
        }

        [Fact]
        public void DeleteEmployeePositiveTest()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();
            var employee = testDataGenerator.GenerateEmployees(1).First();
            storage.Add(employee);

            // Act
            storage.Delete(employee);
            var actualEmployees = storage.Get(null);

            // Assert
            Assert.DoesNotContain(employee, actualEmployees);
        }

        [Fact]
        public void GetEmployeePositiveTest()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();
            var employees = testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                storage.Add(employee);
            }
            var employeeToFind = employees.First();

            // Act
            var foundEmployee = storage.Get(e => e.PassportNumber == employeeToFind.PassportNumber).FirstOrDefault();

            // Assert
            Assert.NotNull(foundEmployee);
            Assert.Equal(employeeToFind, foundEmployee);
        }
    }
}
