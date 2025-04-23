using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Backend.Models;

namespace Backend.Services
{
    public class PredictionService
    {
        private readonly InferenceSession _session;

        public PredictionService()
        {
            _session = new InferenceSession("ML/best.onnx");
        }

        public async Task<PredictionResult> PredictAsync(string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            using var ms = new MemoryStream(imageBytes);
            using var bitmap = new Bitmap(ms);

            using var resized = new Bitmap(bitmap, new Size(640, 640));

            var inputTensor = new DenseTensor<float>(new[] { 1, 3, 640, 640 });

            for(int y = 0; y < 640; y++)
            {
                for(int x = 0; x < 640; x++)
                {
                    var pixel = resized.GetPixel(x, y);
                    inputTensor[0, 0, y, x] = pixel.R / 255f;
                    inputTensor[0, 1, y, x] = pixel.G / 255f;
                    inputTensor[0, 2, y, x] = pixel.B / 255f;
                }
            }

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("images", inputTensor)
            };

            using var results = _session.Run(inputs);
            var output = results.First().AsEnumerable<float>().ToArray();
            return new PredictionResult
            {
                RawOutput = output.Take(5).ToArray()
            };

        }
    }
}
