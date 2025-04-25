using System.Text.Json.Serialization;

namespace Backend.Models
{
        public class PredictionResult
        {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("output_image")]
        public string OutputImage { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }
    }
}
