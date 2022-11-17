using System.Text.Json.Serialization;

namespace DuploParser.Models.Api
{
    public class Stock
    {
        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }
    }
}
