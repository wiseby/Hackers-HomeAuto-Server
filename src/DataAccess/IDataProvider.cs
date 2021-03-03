using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataProvider<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Update(int id, T entity);
        Task<int> Remove(int id);
        Task<T> Create(T entity);
    }
}