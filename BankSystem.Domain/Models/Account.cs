namespace BankSystem.Domain.Models
{
    public class Account
    {
        public Currency Currency { get; init; }
        public decimal Amount { get; set; }
    }
}
