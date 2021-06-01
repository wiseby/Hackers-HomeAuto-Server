using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Repositries
{
    public interface INodeRepository : ICrudRepository<Node>
    {
        Task<IEnumerable<Node>> ReadAllPending(CancellationToken cancellationToken);
        Task<Node> UpdatePending(Node node, CancellationToken cancellationToken);
        Task<Node> DeletePending(Node node, CancellationToken cancellationToken);
    }
}