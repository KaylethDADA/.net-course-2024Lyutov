namespace BankSystem.Application.Dto.Currency
{
    public class CurrencyConvertResponse
    {
        public decimal Amount { get; set; }
        public int Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
