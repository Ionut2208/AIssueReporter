using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PredictionRequest
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
