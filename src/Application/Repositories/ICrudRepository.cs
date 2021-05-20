using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositries
{
    public interface ICrudRepository<T>
    {
        Task<T> Create(T entity);
        Task<IEnumerable<T>> ReadAll();
        Task<T> ReadById(string id);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}