namespace BankSystem.Application.Exceptions
{
    public class EmployeeException : Exception
    {
        public EmployeeException(string message) : base(message)
        {
        }

        public EmployeeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
