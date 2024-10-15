namespace BankSystem.Domain.Models
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
