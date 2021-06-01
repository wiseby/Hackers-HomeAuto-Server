using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Services
{
    public interface INodeService
    {

        Task<IEnumerable<Node>> GetNodes(CancellationToken cancellationToken);
        Task<Node> GetNodeById(string clientId, CancellationToken cancellationToken);
        Task<IEnumerable<Reading>> GetReadingsById(string clientId, CancellationToken cancellationToken);
        Task<IEnumerable<ReadingDefinition>> UpdateDefinitions(
            string clientId, IEnumerable<ReadingDefinition> definitions, CancellationToken cancellationToken);
        Task<IEnumerable<ReadingDefinition>> GetDefinitions(CancellationToken cancellationToken);
        Task<IEnumerable<ReadingDefinition>> GetDefinitionsByClientId(string clientId, CancellationToken cancellationToken);
    }
}