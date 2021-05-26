using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    public class ReadingDefinition
    {
        [BsonId]
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