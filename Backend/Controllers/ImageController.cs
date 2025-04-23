using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController: ControllerBase
    {
        private readonly PredictionService _predictionService;

        public ImageController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpPost("predict")]
        public async Task<IActionResult> Predict([FromBody] PredictionRequest request)
        {
            var result = await _predictionService.PredictAsync(request.ImageBase64);
            return Ok(result);
        }
    }
}
