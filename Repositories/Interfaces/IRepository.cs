namespace OOPShop.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        bool Delete(int id);
        void Save(T entity);
        void Save();
    }
}