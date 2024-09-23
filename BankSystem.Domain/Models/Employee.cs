namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public Employee(string fullName, string phoneNumber, DateTime birthDay)
            : base(fullName, phoneNumber, birthDay)
        {

        }

        public Employee()
        {
        
        }

        public int Salary { get; set; }

        public string Contract { get; set; }
    }
}
