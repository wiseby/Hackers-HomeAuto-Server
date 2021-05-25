using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class JsonResponse<T>
    {
        [JsonInclude]
        public T Data { get; set; }
    }
}