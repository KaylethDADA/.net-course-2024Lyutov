namespace BankSystem.Domain.Models
{
    /// <summary>
    /// Абстрактный класс, представляющий человека.
    /// </summary>
    public class Person
    {
        public Person(string fullName, string phoneNumber, DateTime birthDay)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            BirthDay = birthDay;
        }

        /// <summary>
        /// ФИО человека.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age => DateTime.Now.Year - BirthDay.Year;
    }
}
