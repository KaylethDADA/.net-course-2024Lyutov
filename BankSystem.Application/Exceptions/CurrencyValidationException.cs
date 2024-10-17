namespace BankSystem.Application.Exceptions
{
    public class CurrencyValidationException : CurrencyException
    {
        public CurrencyValidationException(string message) : base(message)
        {
        }
    }
}
