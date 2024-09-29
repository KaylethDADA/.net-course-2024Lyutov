namespace BankSystem.Domain.Models
{
    public struct Currency
    {
        public Currency(string code, string name, string symdol)
        {
            Code = code;
            Name = name;
            Symbol = symdol;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
