namespace BankSystem.Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportNumber { get; set; }

        public int Age => DateTime.Now.Year - BirthDay.Year - (DateTime.Now.DayOfYear < BirthDay.DayOfYear ? 1 : 0);
    }
}
