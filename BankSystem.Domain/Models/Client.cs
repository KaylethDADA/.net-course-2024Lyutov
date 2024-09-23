namespace BankSystem.Domain.Models
{
    /// <summary>
    /// Класс, представляющий клиента.
    /// </summary>
    public class Client : Person
    {
        public Client(string fullName, string phoneNumber, DateTime birthDay)
            : base(fullName, phoneNumber, birthDay)
        {
        }
    }
}
