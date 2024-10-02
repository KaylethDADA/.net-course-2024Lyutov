using BankSystem.Application.Services;
using BankSystem.Data.Storages;

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
            var actualEmployees = storage.Employees;

            // Assert
            Assert.NotNull(actualEmployees);
            Assert.True(employees.SequenceEqual(actualEmployees));
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
    }
}
