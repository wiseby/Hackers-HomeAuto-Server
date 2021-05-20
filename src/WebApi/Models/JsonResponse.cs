using System.Collections.Generic;

namespace WebApi.Models
{
    public class JsonResponse
    {
        public JsonData Data { get; set; }
    }

    public class JsonData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
    }
}