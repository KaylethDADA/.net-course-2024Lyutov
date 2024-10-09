namespace BankSystem.Application.Interfaces
{
    public interface IStorage<T, TResult>
    {
        void Add(T item); 
        void Update(T item);
        TResult Get(Func<T, bool>? filter);
        void Delete(T item);
    }
}
