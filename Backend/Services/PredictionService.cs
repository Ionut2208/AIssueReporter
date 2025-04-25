using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Backend.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Json;

namespace Backend.Services
{
    public class PredictionService
    {
        private readonly InferenceSession _session;

        public PredictionService()
        {
            _session = new InferenceSession("ML/best.onnx");
        }


        public async Task<PredictionResult> PredictUsingPythonAsync(IFormFile image)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(image.OpenReadStream()), "file", image.FileName);

            using var client = new HttpClient();
            var response = await client.PostAsync("http://127.0.0.1:8000/detect/", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PredictionResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

    }
}
