namespace BankSystem.Domain.Models
{
    /// <summary>
    /// Класс, представляющий клиента.
    /// </summary>
    public class Client : Person
    {
        public Client(string fullName, string phoneNumber, DateTime birthDay, DateTime registrationDate)
            : base(fullName, phoneNumber, birthDay)
        {
            RegistrationDate = registrationDate;
        }

        /// <summary>
        /// Дата регистрации клиента.
        /// </summary>
        public DateTime RegistrationDate { get; set; }
    }
}
