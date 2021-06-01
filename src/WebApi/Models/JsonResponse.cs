using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class JsonOkResponse<T>
    {
        [JsonInclude]
        public T Data { get; set; }

        public JsonOkResponse(T data)
        {
            this.Data = data;
        }
    }

    public class JsonErrorResponse
    {
        [JsonInclude]
        public Dictionary<string, string> Errors { get; set; }

        public JsonErrorResponse(Dictionary<string, string> errors)
        {
            this.Errors = errors;
        }
    }

    public class JsonRequest<T>
    {
        public T Data { get; set; }
    }
}