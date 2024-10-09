using BankSystem.Domain.Models;

namespace BankSystem.Application.Interfaces
{
    public interface IEmployeeStorage : IStorage<Employee, List<Employee>>
    {
    }
}
