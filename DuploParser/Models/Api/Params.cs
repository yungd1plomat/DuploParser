using System.Text.Json.Serialization;

namespace DuploParser.Models.Api
{
    public class Params
    {
        [JsonPropertyName("season")]
        public Season Season { get; set; }

        [JsonPropertyName("runflat")]
        public bool RunFlat { get; set; }

        [JsonPropertyName("pins")]
        public bool Pins { get; set; }
    }
}
