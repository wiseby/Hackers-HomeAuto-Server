using System;
using System.Threading.Tasks;
using Application.Models;
using DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace Application.MqttContextHandler
{
    public class MqttContextHandler : IMqttContextHandler
    {
        private readonly MongoClient mongoClient;
        private readonly ILogger logger;
        private readonly string database;

        public MqttContextHandler(MongoConnection connection, ILogger logger)
        {
            this.mongoClient = connection.GetConnection();
            this.logger = logger;
            this.database = "hha_dev";
        }

        public async Task SaveContext(Context context)
        {
            if (!(await IsConfigured(context.ClientId)))
            {
                await SavePendingNode(context);
            }
            await SaveReading(context);
        }

        private async Task<bool> IsConfigured(string clientId)
        {
            var database = mongoClient.GetDatabase(this.database);
            var collection = database.GetCollection<Node>("Nodes");
            var builder = Builders<Node>.Filter;
            var filter = builder.Eq(node => node.ClientId, clientId) & builder.Eq(node => node.IsConfigured, true);
            var result = await collection.Find(filter).FirstOrDefaultAsync();
            return result != null;
        }

        private async Task SavePendingNode(Context context)
        {
            var database = mongoClient.GetDatabase(this.database);
            var collection = database.GetCollection<Node>("Nodes");
            var builder = Builders<Node>.Filter;
            var filter = builder.Eq(node => node.ClientId, context.ClientId) & builder.Eq(node => node.IsConfigured, false);
            var result = await collection.Find(filter).FirstOrDefaultAsync();

            if (result == null)
            {
                var node = new Node()
                {
                    ClientId = context.ClientId,
                    IsConfigured = false,
                    CreatedAt = DateTime.UtcNow
                };
                await collection.InsertOneAsync(node);
            }
        }

        private Task SaveReading(Context context)
        {
            var database = mongoClient.GetDatabase(this.database);
            var collection = database.GetCollection<Reading>("Readings");
            var valuesBson = new BsonDocument();
            valuesBson.AddRange(context.Payload);
            var reading = new Reading()
            {
                ClientId = context.ClientId,
                Values = context.Payload,
                CreatedAt = DateTime.UtcNow
            };
            return collection.InsertOneAsync(reading);
        }
    }
}