using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class ReadingDefinitionDto
    {
        [JsonIgnore]
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Unit { get; set; }
    }
}