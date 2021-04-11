using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using DataAccess;
using MongoDB.Bson;
using MQTTnet;
using MQTTnet.Server;
using Serilog;

namespace MqttServer
{
    public class MessageInterceptor
    {
        private readonly ILogger logger;
        private readonly MongoConnection connection;

        public MessageInterceptor(ILogger logger, MongoConnection connection)
        {
            this.logger = logger;
            this.connection = connection;
        }


        public void Intercept(MqttApplicationMessageInterceptorContext context)
        {
            Log.Information($"Intercepting message");
            var payload = JsonSerializer.Deserialize<Dictionary<string, string>>(context.ApplicationMessage.Payload);
            Log.Information($"Topic: {context.ApplicationMessage.Topic} | Payload: {payload["msg"]}");
            context.ApplicationMessage.Payload = Encoding.UTF8.GetBytes("Server injected payload");

            SaveMessage(context.ApplicationMessage);
        }

        private async void SaveMessage(MqttApplicationMessage message)
        {
            logger.Information("Saving to database");
            var database = connection.MongoClient.GetDatabase("mqtt_topics");
            var collection = database.GetCollection<BsonDocument>("messages");
            var document = new BsonDocument
            {
                { "topic", message.Topic },
                { "message", "saving topic" },
                { "createdat", DateTime.Now }
            };
            await collection.InsertOneAsync(document);

            var count = collection.CountDocuments(new BsonDocument());
            logger.Information($"Number of entries in db: {count}");
        }
    }
}