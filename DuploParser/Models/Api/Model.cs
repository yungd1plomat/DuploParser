using System.Text.Json.Serialization;

namespace DuploParser.Models.Api
{
    public class Model
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("name")]
        public string LogoUrl { get; set; }

        [JsonPropertyName("url")]
        public string UrlPath { get; set; }
    }
}
