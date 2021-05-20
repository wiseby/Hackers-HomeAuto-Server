using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    public class Reading : IClientEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("clientId")]
        public string ClientId { get; set; }
        [BsonElement("values")]
        public Dictionary<string, object> Values { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}