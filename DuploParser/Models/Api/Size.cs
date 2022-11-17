using System.Text.Json.Serialization;

namespace DuploParser.Models.Api
{
    public class Size
    {
        [JsonPropertyName("radius")]
        public int Radius { get; set; }

        [JsonPropertyName("profile")]
        public int Profile { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }
    }
}
