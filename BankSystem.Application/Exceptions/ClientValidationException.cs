namespace BankSystem.Application.Exceptions
{
    public class ClientValidationException : ClientException
    {
        public ClientValidationException(string message) : base(message)
        {
        }
    }
}
