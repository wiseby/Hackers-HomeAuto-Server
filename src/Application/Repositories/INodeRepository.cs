using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Repositries
{
    public interface INodeRepository : ICrudRepository<Node>
    {
        Task<IEnumerable<Node>> ReadAllPending();
        Task<Node> UpdatePending(Node node);
        Task<Node> DeletePending(Node node);
    }
}