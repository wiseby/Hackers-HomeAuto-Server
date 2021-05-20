using Application.Models;
using DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositries
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly AppOptions configuration;
        private readonly IMongoClient mongoClient;

        public ReadingRepository(IMongoConnection connection, AppOptions configuration)
        {
            this.configuration = configuration;
            this.mongoClient = connection.GetConnection();
        }

        public Task<Reading> Create(Reading entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Reading> Delete(Reading entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Reading>> GetAllByClientId(string clientId)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var readingsCollection = database.GetCollection<Reading>("Readings");
            var builder = Builders<Reading>.Filter;
            var filter = builder.Eq(reading => reading.ClientId, clientId);

            try
            {
                return await readingsCollection.Find(filter).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load readings", e);
            }
        }

        public async Task<Reading> GetLatestByClientId(string clientId)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var readingsCollection = database.GetCollection<Reading>("Readings");
            var builder = Builders<Reading>.Filter;
            var filter = builder.Eq(reading => reading.ClientId, clientId);

            try
            {
                return await readingsCollection.Find(filter).SortBy(reading => reading.CreatedAt).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load readings", e);
            }
        }

        public async Task<IEnumerable<Reading>> ReadAll()
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var readingsCollection = database.GetCollection<Reading>("Readings");
            var filter = new BsonDocument();

            try
            {
                return await readingsCollection.Find(filter).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load readings", e);
            }
        }

        public Task<Reading> ReadById(string clientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Reading> Update(Reading entity)
        {
            throw new System.NotImplementedException();
        }
    }
}