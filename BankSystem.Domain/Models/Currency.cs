namespace BankSystem.Domain.Models
{
    /// <summary>
    /// Структура, представляющая валюту.
    /// </summary>
    public struct Currency
    {
        public Currency(string code, decimal amount)
        {
            Code = code;
            Amount = amount;
        }

        /// <summary>
        /// Код валюты (например: USD, EUR, RUB).
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Сумма денег в данной валюте.
        /// </summary>
        public decimal Amount { get; init; }
    }
}
