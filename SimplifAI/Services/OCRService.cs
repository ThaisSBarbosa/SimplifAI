using SimplifAI.Utils;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace SimplifAI.Services
{
    public static class OCRService
    {
        public static string LeDocumento(string caminho)
        {
            var configuracao = Helper.GetConfiguracoes();
            string key = configuracao["OCR_API_KEY"];
            string endpoint = configuracao["OCR_ENDPOINT"];

            AzureKeyCredential credential = new AzureKeyCredential(key);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            //Stream arquivoStream = Helper.RetornaStreamRecursoIncorporado("Resources.Images.exemplo.png");
            Stream arquivoStream = FileHelper.RetornaStreamArquivo(caminho);
            AnalyzeDocumentOperation operation = client.AnalyzeDocument(WaitUntil.Completed, "prebuilt-layout", arquivoStream);
            
            return operation.Value.Content;
        }

        public static string LeDocumentoRecurso(string caminho)
        {
            var configuracao = Helper.GetConfiguracoes();
            string key = configuracao["OCR_API_KEY"];
            string endpoint = configuracao["OCR_ENDPOINT"];

            AzureKeyCredential credential = new AzureKeyCredential(key);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            Stream arquivoStream = Helper.RetornaStreamRecursoIncorporado("Resources.Images."+caminho);
            //Stream arquivoStream = FileHelper.RetornaStreamArquivo(caminho);
            AnalyzeDocumentOperation operation = client.AnalyzeDocument(WaitUntil.Completed, "prebuilt-layout", arquivoStream);

            return operation.Value.Content;
        }
    }
}
