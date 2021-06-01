using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    [BsonIgnoreExtraElements]
    public class ReadingDefinition : IClientEntity
    {
        [BsonElement("clientId")]
        public string ClientId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("icon")]
        public string Icon { get; set; }
        [BsonElement("unit")]
        public string Unit { get; set; }
    }
}