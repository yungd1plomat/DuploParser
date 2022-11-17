using System.Text.Json.Serialization;

namespace DuploParser.Models.Api
{
    public class Brand
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string UrlPath { get; set; }
    }
}
