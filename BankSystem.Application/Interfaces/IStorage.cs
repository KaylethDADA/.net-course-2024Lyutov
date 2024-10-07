namespace BankSystem.Application.Interfaces
{
    public interface IStorage<TType>
    {
        void Add(TType item); 
        void Update(TType item);
        List<TType> Get(Func<TType, bool>? filter);
        void Delete(TType item);
    }
}
