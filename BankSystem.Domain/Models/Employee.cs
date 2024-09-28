namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public Employee(string fullName, string phoneNumber, DateTime birthDay, string contract, int salary)
            : base(fullName, phoneNumber, birthDay)
        {
            Contract = contract;
            Salary = salary;
        }

        public Employee()
        {
        
        }

        public int Salary { get; set; }

        public string Contract { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Employee employee))
                return false;

            return FullName == employee.FullName &&
                   BirthDay == employee.BirthDay &&
                   Contract == employee.Contract &&
                   Salary == employee.Salary;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, BirthDay, Contract, Salary);
        }
    }
}
