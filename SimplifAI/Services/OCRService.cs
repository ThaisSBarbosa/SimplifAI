using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using SimplifAI.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SimplifAI.Services
{
    public static class OCRService
    {
        public static async Task<string> LeImagem(string pathImagem = null)
        {
            var configuracao = Helper.GetConfiguracoes();
            string key = configuracao["OCR_API_KEY"];
            string endpoint = configuracao["OCR_ENDPOINT"];

            ComputerVisionClient computerVisionClient = Authenticate(endpoint, key);
            return await ReadFile(computerVisionClient);
        }

        private static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        private static async Task<string> ReadFile(ComputerVisionClient client)
        {
            byte[] bytes = ConvertImagemTesteBytes();
            string base64String = Convert.ToBase64String(bytes);
            byte[] decodedImageBytes = Convert.FromBase64String(base64String);

            ReadInStreamHeaders textHeaders = null;
            using (MemoryStream imageStream = new MemoryStream(decodedImageBytes))
                textHeaders = await client.ReadInStreamAsync(imageStream);

            string result = string.Empty;

            if (textHeaders != null)
            {
                string operationLocation = textHeaders.OperationLocation;

                // Retrieve the URI where the extracted text will be stored from the Operation-Location header.
                // We only need the ID and not the full URL
                const int numberOfCharsInOperationId = 36;
                string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

                ReadOperationResult results;
                do
                {
                    results = await client.GetReadResultAsync(Guid.Parse(operationId));
                }
                while ((results.Status == OperationStatusCodes.Running ||
                    results.Status == OperationStatusCodes.NotStarted));

                var textUrlFileResults = results.AnalyzeResult.ReadResults;
                foreach (ReadResult page in textUrlFileResults)
                {
                    foreach (Line line in page.Lines)
                        result += line.Text;
                }
            }

            return result;
        }

        private static byte[] ConvertImagemTesteBytes()
        {
            using (Stream stream = Helper.RetornaStreamRecursoIncorporado("Resources.Images.printed_text.jpg"))
            {
                if (stream == null)
                    return null;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
