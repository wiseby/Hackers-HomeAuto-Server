using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Repositries
{
    public interface IReadingRepository : ICrudRepository<Reading>
    {
        Task<IEnumerable<Reading>> GetAllByClientId(string clientId, CancellationToken cancellationToken);
        Task<Reading> GetLatestByClientId(string clientId, CancellationToken cancellationToken);
        Task<long> GetReadingCount(string clientId, CancellationToken cancellationToken);
        Task<IEnumerable<ReadingDefinition>> GetReadingDefinitions(CancellationToken cancellationToken);
        Task<IEnumerable<ReadingDefinition>> GetReadingDefinitionsByClientId(
            string clientId, CancellationToken cancellationToken);
        Task<long> UpdateReadingDefinitions(
            string clientId, List<ReadingDefinition> definitions, CancellationToken cancellationToken);
    }
}