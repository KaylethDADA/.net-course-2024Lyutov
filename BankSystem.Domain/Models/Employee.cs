namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public int Salary { get; set; }

        public string Contract { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Employee employee))
                return false;

            return PassportNumber == employee.PassportNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PassportNumber);
        }
    }
}
