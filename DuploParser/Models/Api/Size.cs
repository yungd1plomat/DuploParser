using System.Text.Json.Serialization;

namespace DuploParser.Models.Api
{
    public class Size
    {
        [JsonPropertyName("radius")]
        public double Radius { get; set; }

        [JsonPropertyName("profile")]
        public double Profile { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }
    }
}
