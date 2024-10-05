namespace BankSystem.Domain.Models
{
    public struct Currency
    {
        public Currency(string code, string symbol, string name)
        {
            Code = code;
            Name = name;
            Symbol = symbol;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
