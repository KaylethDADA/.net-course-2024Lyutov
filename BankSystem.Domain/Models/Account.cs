namespace BankSystem.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
