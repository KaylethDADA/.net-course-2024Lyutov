using System.Linq.Expressions;

namespace BankSystem.Application.Interfaces
{
    public interface IStorage<T>
    {
        Task AddAsync(T item, CancellationToken cancellationToken); 
        Task UpdateAsync(T item, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>>? filter, int? pageNumber, int? pageSize, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
