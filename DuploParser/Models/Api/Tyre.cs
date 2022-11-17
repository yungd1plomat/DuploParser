using System.Text.Json.Serialization;

namespace DuploParser.Models.Api
{
    public class Tyre
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("size")]
        public Size Size { get; set; }

        [JsonPropertyName("sizeText")]
        public string SizeText { get; set; }

        [JsonPropertyName("brand")]
        public Brand Brand { get; set; }

        [JsonPropertyName("model")]
        public Model Model { get; set; }

        [JsonPropertyName("stock")]
        public Stock Stock { get; set; }
    }
}
