using System.Text.Json.Serialization;

namespace DuploParser.Models.Api
{
    public class Season
    {
        [JsonPropertyName("text")]
        public string Name { get; set; }
    }
}
