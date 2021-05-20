using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    public class Context : IClientEntity
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        [BsonIgnore]
        public string Topic { get; set; }
        public Dictionary<string, object> Payload { get; set; }
    }
}