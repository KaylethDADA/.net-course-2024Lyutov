using System.Linq.Expressions;

namespace BankSystem.Application.Interfaces
{
    public interface IStorage<T>
    {
        void Add(T item); 
        void Update(T item);
        ICollection<T> Get(Expression<Func<T, bool>> filter, int pageNumber, int pageSize);
        void Delete(Guid id);
    }
}
