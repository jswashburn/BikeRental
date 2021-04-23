using System.Collections.Generic;

namespace BikeRental.Models.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> Get();
        T Get(int id);
        T Insert(T item);
        T Delete(int id);
        T Update(T item);
        void Save();
    }
}
