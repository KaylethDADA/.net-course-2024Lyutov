using BankSystem.Domain.Models;

namespace BankSystem.Application.Interfaces
{
    public interface ICurrencyStorage : IStorage<Currency>
    {
        Currency GetDefaultCurrency();
    }
}