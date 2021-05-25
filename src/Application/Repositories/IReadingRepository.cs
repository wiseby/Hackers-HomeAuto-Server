using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Repositries
{
    public interface IReadingRepository : ICrudRepository<Reading>
    {
        Task<IEnumerable<Reading>> GetAllByClientId(string clientId);
        Task<Reading> GetLatestByClientId(string clientId);
        Task<long> GetReadingCount(string clientId);
    }
}