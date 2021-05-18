using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.MqttContextHandler
{
    public class ContextModel
    {
        public string ClientId { get; set; }
        [BsonIgnore]
        public string Topic { get; set; }
        public Dictionary<string, object> Payload { get; set; }
    }

    public class Node
    {
        [BsonElement("clientId")]
        [BsonId]
        public string ClientId { get; set; }
        [BsonElement("isConfigured")]
        public bool IsConfigured { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    public class Reading
    {
        [BsonElement("clientId")]
        public string ClientId { get; set; }
        [BsonElement("values")]
        public Dictionary<string, object> Values { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}