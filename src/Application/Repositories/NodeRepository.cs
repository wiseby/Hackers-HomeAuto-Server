using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Repositries
{
    public class NodeRepository : INodeRepository
    {
        private readonly AppOptions configuration;
        private readonly IMongoClient mongoClient;

        public NodeRepository(IMongoConnection connection, AppOptions configuration)
        {
            this.configuration = configuration;
            this.mongoClient = connection.GetConnection();
        }
        public async Task<Node> Create(Node entity)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var collection = database.GetCollection<Node>("Nodes");

            var node = new Node()
            {
                ClientId = entity.ClientId,
                IsConfigured = false,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                await collection.InsertOneAsync(node);
                return node;
            }
            catch (MongoDuplicateKeyException e)
            {
                var message = $"Node with clientId: {node.ClientId} already exists";
                throw new Exception(message, e);
            }
        }

        public async Task<Node> Delete(Node node)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var collection = database.GetCollection<Node>("Nodes");

            try
            {
                var result = await collection.DeleteOneAsync((node) => node.ClientId == node.ClientId);
                if (result.DeletedCount == 1)
                {
                    return node;
                }

                return null;
            }
            catch (MongoException e)
            {
                var message = $"Failed to delete node.\n{e.Message}";
                throw new Exception(message, e);
            }
        }

        public async Task<Node> DeletePending(Node node)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var nodesCollection = database.GetCollection<Node>("Nodes");
            var readingsCollection = database.GetCollection<Reading>("Readings");

            // Delete all readings with clientId
            var readingsFilter = GetClientIdFilter<Reading>(node.ClientId);

            // Finally delete definition of the pending node
            var nodesFilter = GetClientIdFilter<Node>(node.ClientId);

            try
            {
                await readingsCollection.DeleteManyAsync(readingsFilter);
                await nodesCollection.DeleteManyAsync(nodesFilter);
                return node;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to delete Pending Node with clientId: {node.ClientId}", e);
            }
        }

        public async Task<IEnumerable<Node>> ReadAll()
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var nodesCollection = database.GetCollection<Node>("Nodes");
            var filter = new BsonDocument();

            try
            {
                return await nodesCollection.Find(filter).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load configured Nodes", e);
            }
        }

        public async Task<IEnumerable<Node>> ReadAllPending()
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var nodesCollection = database.GetCollection<Node>("Nodes");
            var filter = GetPendingFilter();

            try
            {
                return await nodesCollection.Find(filter).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load pending Nodes", e);
            }
        }

        public Task<Node> ReadById(string id)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var nodesCollection = database.GetCollection<Node>("Nodes");
            var filter = GetClientIdFilter<Node>(id);

            try
            {
                return nodesCollection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load Node with id: {id}", e);
            }
        }

        public async Task<Node> Update(Node node)
        {
            var database = mongoClient.GetDatabase(this.configuration.Database);
            var nodesCollection = database.GetCollection<Node>("Nodes");
            var filter = GetClientIdFilter<Node>(node.ClientId);

            var options = new FindOneAndReplaceOptions<Node>
            {
                ReturnDocument = ReturnDocument.Before
            };

            try
            {
                return await nodesCollection.FindOneAndReplaceAsync(filter, node, options);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to update Node with id: {node.ClientId}", e);
            }

        }

        public Task<Node> UpdatePending(Node node)
        {
            // Make a pending configured
            // Add additional data for all values it represents
            throw new System.NotImplementedException();
        }

        private FilterDefinition<T> GetClientIdFilter<T>(string clientId) where T : IClientEntity
        {
            var builder = Builders<T>.Filter;
            return builder.Eq(node => node.ClientId, clientId);
        }

        private FilterDefinition<Node> GetPendingFilter()
        {
            var builder = Builders<Node>.Filter;
            return builder.Eq(entity => entity.IsConfigured, false);
        }
    }
}