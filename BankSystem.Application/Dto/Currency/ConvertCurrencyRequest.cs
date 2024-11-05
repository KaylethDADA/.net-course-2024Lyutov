namespace BankSystem.Application.Dto.Currency
{
    public class ConvertCurrencyRequest
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
