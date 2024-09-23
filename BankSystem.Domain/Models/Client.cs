namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public Client(string fullName, string phoneNumber, DateTime birthDay)
            : base(fullName, phoneNumber, birthDay)
        {
        }

        public Client()
        {
        
        }

    }
}
