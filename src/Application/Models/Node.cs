using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    public class Node : IClientEntity
    {
        [BsonElement("clientId")]
        [BsonId]
        public string ClientId { get; set; }
        [BsonElement("isConfigured")]
        public bool IsConfigured { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}