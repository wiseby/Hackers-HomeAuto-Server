using Application.Models;
using DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public Task<Reading> Create(Reading entity, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Reading> Delete(Reading entity, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Reading>> GetAllByClientId(
            string clientId, CancellationToken cancellationToken)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var readingsCollection = database.GetCollection<Reading>("Readings");
            var builder = Builders<Reading>.Filter;
            var filter = builder.Eq(reading => reading.ClientId, clientId);

            try
            {
                return await readingsCollection.Find(filter).ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load readings", e);
            }
        }

        public async Task<Reading> GetLatestByClientId(string clientId, CancellationToken cancellationToken)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var readingsCollection = database.GetCollection<Reading>("Readings");
            var builder = Builders<Reading>.Filter;
            var filter = builder.Eq(reading => reading.ClientId, clientId);

            try
            {
                return await readingsCollection.Find(filter).SortBy(reading => reading.CreatedAt).FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load readings", e);
            }
        }

        public async Task<long> GetReadingCount(string clientId, CancellationToken cancellationToken)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var readingsCollection = database.GetCollection<Reading>("Readings");
            var builder = Builders<Reading>.Filter;
            var filter = builder.Eq(reading => reading.ClientId, clientId);


            try
            {
                return await readingsCollection.Find(filter).CountDocumentsAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to count readings", e);
            }
        }

        public async Task<IEnumerable<ReadingDefinition>> GetReadingDefinitions(CancellationToken cancellationToken)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var definitionsCollection = database.GetCollection<ReadingDefinition>("ReadingDefinitions");
            var filter = new BsonDocument();

            try
            {
                return await definitionsCollection.Find(filter).ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load definitions", e);
            }
        }

        public async Task<IEnumerable<ReadingDefinition>> GetReadingDefinitionsByClientId(
            string clientId, CancellationToken cancellationToken)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var definitionsCollection = database.GetCollection<ReadingDefinition>("ReadingDefinitions");
            var builder = Builders<ReadingDefinition>.Filter;
            var filter = builder.Eq(definition => definition.ClientId, clientId);

            try
            {
                return await definitionsCollection.Find(filter).ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load definitions", e);
            }
        }

        private async Task<long> CreateReadingDefinitions(
            IMongoCollection<ReadingDefinition> collection,
            string clientId, List<ReadingDefinition> definitions,
            CancellationToken cancellationToken)
        {
            var filterBuilder = Builders<ReadingDefinition>.Filter;
            var filter = filterBuilder.Eq(definition => definition.ClientId, clientId);

            var createModels = new List<InsertOneModel<ReadingDefinition>>();
            foreach (var definition in definitions)
            {
                var model = new InsertOneModel<ReadingDefinition>(definition);
                createModels.Add(model);
            }

            var options = new BulkWriteOptions()
            {
                BypassDocumentValidation = false,
                IsOrdered = false
            };

            try
            {
                var result = await collection.BulkWriteAsync(createModels, options, cancellationToken);
                return result.ModifiedCount;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to create definitions", e);
            }
        }

        private async Task<long> ReplaceDocumentsByClientId<T>(
            IMongoCollection<T> collection,
            FilterDefinition<T> clientIdFilter,
            List<T> documents,
            CancellationToken cancellationToken) where T : IClientEntity
        {
            var updateModels = new List<WriteModel<T>>();
            foreach (var document in documents)
            {
                var model = new ReplaceOneModel<T>(clientIdFilter, document);
                updateModels.Add(model);
            }

            var options = new BulkWriteOptions()
            {
                BypassDocumentValidation = false,
                IsOrdered = false
            };

            try
            {
                var result = await collection.BulkWriteAsync(updateModels, options, cancellationToken);
                return result.ModifiedCount;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to update readings", e);
            }
        }

        public async Task<long> UpdateReadingDefinitions(
            string clientId,
            List<ReadingDefinition> definitions,
            CancellationToken cancellationToken)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var collection = database.GetCollection<ReadingDefinition>("ReadingDefinitions");
            var filterBuilder = Builders<ReadingDefinition>.Filter;
            var clientIdFilter = filterBuilder.Eq(definition => definition.ClientId, clientId);

            var existingDefinitions = await collection.Find(clientIdFilter).ToListAsync();

            var dbDefsDict = existingDefinitions.ToDictionary((key) => key.Name);

            var bulkModels = new List<WriteModel<ReadingDefinition>>();

            foreach (var def in definitions)
            {
                if (dbDefsDict.TryGetValue(def.Name, out ReadingDefinition value))
                {
                    var filter = Builders<ReadingDefinition>.Filter.Eq(d => d.Name, def.Name);
                    var update = Builders<ReadingDefinition>.Update
                        .Set(d => d.Name, def.Name)
                        .Set(d => d.Icon, def.Icon)
                        .Set(d => d.Unit, def.Unit);
                    bulkModels.Add(new UpdateOneModel<ReadingDefinition>(filter, update));
                }
                else
                {
                    bulkModels.Add(new InsertOneModel<ReadingDefinition>(def));
                }
            }

            var options = new BulkWriteOptions()
            {
                BypassDocumentValidation = false,
                IsOrdered = false
            };

            try
            {
                var result = await collection.BulkWriteAsync(bulkModels, options, cancellationToken);
                return result.ModifiedCount;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to update readings", e);
            }
        }

        public async Task<IEnumerable<Reading>> ReadAll(CancellationToken cancellationToken)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var readingsCollection = database.GetCollection<Reading>("Readings");
            var filter = new BsonDocument();

            try
            {
                return await readingsCollection.Find(filter).ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load readings", e);
            }
        }

        public Task<Reading> ReadById(string clientId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Reading> Update(Reading entity, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}