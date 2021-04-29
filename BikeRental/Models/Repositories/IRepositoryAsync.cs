using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeRental.Models.Repositories
{
    public interface IRepositoryAsync<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAsync(string controller);
        Task<T> GetAsync(int id, string controller);
        Task<T> InsertAsync(T item, string controller);
        Task<T> DeleteAsync(int id, string controller);
        Task<T> UpdateAsync(T item, string controller);
    }
}
