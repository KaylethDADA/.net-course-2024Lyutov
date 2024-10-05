namespace BankSystem.Application.Exceptions
{
    public class EmployeeValidationException : EmployeeException
    {
        public EmployeeValidationException(string message) : base(message)
        {
            
        }
    }
}
