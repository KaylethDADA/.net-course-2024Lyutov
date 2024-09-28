namespace BankSystem.Domain.Models
{
    public class Account
    {
        public Account(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public Account()
        {

        }

        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
