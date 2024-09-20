namespace BankSystem.Domain.Models
{
    /// <summary>
    /// Класс, представляющий сотрудника.
    /// </summary>
    public class Employee : Person
    {
        public Employee(string fullName, string phoneNumber, DateTime birthDay)
            : base(fullName, phoneNumber, birthDay)
        {

        }

        /// <summary>
        /// Контракт сотрудника.
        /// </summary>
        public string Contract { get; set; }
    }
}
