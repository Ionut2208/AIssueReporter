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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Predict([FromForm] PredictionRequest request)
        {
            if (request.Image == null || request.Image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            var result = await _predictionService.PredictUsingPythonAsync(request.Image);
            return Ok(result);
        }
    }
}
