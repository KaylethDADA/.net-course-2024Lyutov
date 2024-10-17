namespace BankSystem.Application.Exceptions
{
    public class CurrencyException : Exception
    {
        public CurrencyException(string message) : base(message)
        {
        }

        public CurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
