namespace BankSystem.Domain.Models
{
    public struct Currency
    {
        public Currency(string code, decimal amount)
        {
            Code = code;
            Amount = amount;
        }

        public string Code { get; init; }

        public decimal Amount { get; init; }
    }
}
