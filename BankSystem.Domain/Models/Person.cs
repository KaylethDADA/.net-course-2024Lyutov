namespace BankSystem.Domain.Models
{
    public class Person
    {
        public Person(string fullName, string phoneNumber, DateTime birthDay)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            BirthDay = birthDay;
        }

        public string FullName { get; set; }

        public DateTime BirthDay { get; set; }

        public string PhoneNumber { get; set; }

        public int Age => DateTime.Now.Year - BirthDay.Year;
    }
}
