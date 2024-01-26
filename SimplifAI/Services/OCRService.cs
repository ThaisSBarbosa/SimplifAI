using SimplifAI.Utils;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace SimplifAI.Services
{
    public static class OCRService
    {
        public static string LeImagem(string pathImagem = null)
        {
            var configuracao = Helper.GetConfiguracoes();
            string key = configuracao["OCR_API_KEY"];
            string endpoint = configuracao["OCR_ENDPOINT"];

            AzureKeyCredential credential = new AzureKeyCredential(key);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            Stream imageStream = Helper.RetornaStreamRecursoIncorporado("Resources.Images.exemplo.png");
            AnalyzeDocumentOperation operation = client.AnalyzeDocument(WaitUntil.Completed, "prebuilt-layout", imageStream);

            return operation.Value.Content;
        }
    }
}
