using System;
using System.Collections.Generic;
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
        [BsonIgnore]
        public Reading LatestReading { get; set; }
        [BsonIgnore]
        public IEnumerable<ReadingDefinition> ReadingDefinitions { get; set; }
        [BsonIgnore]
        public long ReadingsAvailable { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}