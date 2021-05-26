using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Services
{
    public interface INodeService
    {

        Task<IEnumerable<Node>> GetNodes();
        Task<Node> GetNodeById(string clientId);
        Task<IEnumerable<Reading>> GetReadingsById(string clientId);
        Task<IEnumerable<ReadingDefinition>> UpdateDefinitions(
            string clientId, ReadingDefinition definition);
        Task<IEnumerable<ReadingDefinition>> GetDefinitions();
        Task<IEnumerable<Reading>> GetDefinitionsByClientId(string clientId);
    }
}