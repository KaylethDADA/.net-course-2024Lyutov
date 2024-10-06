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
                storage.AddEmployee(employee);
            }
            var actualEmployees = storage.GetAllEmployees();

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
            storage.AddEmployee(employee);
            var updatedEmployee = new Employee
            {
                FullName = "Updated Name",
                BirthDay = employee.BirthDay.AddYears(1),
                PhoneNumber = "1234567890",
                Salary = 60000,
                Contract = "Updated Contract"
            };

            // Act
            storage.UpdateEmployee(employee, updatedEmployee);
            var actualEmployee = storage.GetEmployee(updatedEmployee);

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
                storage.AddEmployee(employee);
            }

            // Act
            var allEmployees = storage.GetAllEmployees();

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(employees.Count, allEmployees.Count);
            Assert.True(employees.SequenceEqual(allEmployees));
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
                storage.AddEmployee(employee);
            }
            var employeeToFind = employees.First();

            // Act
            var foundEmployee = storage.GetEmployee(employeeToFind);

            // Assert
            Assert.NotNull(foundEmployee);
            Assert.Equal(employeeToFind, foundEmployee);
        }

        [Fact]
        public void GetYoungestEmployeePositiveTest()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();
            
            var employees = testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                storage.AddEmployee(employee);
            }

            // Act
            var youngest = storage.GetYoungestEmployee();
            var expectedYoungest = employees.OrderBy(x => x.Age)
                    .FirstOrDefault();

            // Assert
            Assert.NotNull(expectedYoungest);
            Assert.Equal(expectedYoungest, youngest);
        }

        [Fact]
        public void GetOldestEmployeePositiveTest()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();

            var employees = testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                storage.AddEmployee(employee);
            }

            // Act
            var oldest = storage.GetOldestEmployee();
            var expectedOldest = employees.OrderByDescending(x => x.Age)
                    .FirstOrDefault();

            // Assert
            Assert.NotNull(expectedOldest);
            Assert.Equal(expectedOldest, oldest);
        }

        [Fact]
        public void GetAverageAgeEmployeePositiveTest()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();

            var employees = testDataGenerator.GenerateEmployees(10);
            foreach (var employee in employees)
            {
                storage.AddEmployee(employee);
            }

            // Act
            var averageAge = storage.GetAverageAgeEmployee();
            var expectedAverageAge = employees.Average(x => x.Age);

            // Assert
            Assert.Equal(expectedAverageAge, averageAge);
        }

        [Fact]
        public void GetEmployeeByFilterPositiveTest()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var testDataGenerator = new TestDataGenerator();

            var employee1 = testDataGenerator.GenerateEmployees(1).First();
            var employee2 = testDataGenerator.GenerateEmployees(1).First();

            storage.AddEmployee(employee1);
            storage.AddEmployee(employee2);

            // Act
            var result = storage.GetEmployeeByFilter(employee1.FullName, null, null, null, null);

            // Assert
            Assert.Single(result);
            Assert.Contains(employee1, result);
        }
    }
}
