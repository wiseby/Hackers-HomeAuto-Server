using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Repositries
{
    public interface ICrudRepository<T>
    {
        Task<T> Create(T entity, CancellationToken cancellationToken);
        Task<IEnumerable<T>> ReadAll(CancellationToken cancellationToken);
        Task<T> ReadById(string id, CancellationToken cancellationToken);
        Task<T> Update(T entity, CancellationToken cancellationToken);
        Task<T> Delete(T entity, CancellationToken cancellationToken);
    }
}