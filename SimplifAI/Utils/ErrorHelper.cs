using System.Net;

namespace SimplifAI.Utils
{
    internal class ErrorHelper
    {

        public static bool checkAcessoServicos()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                return false;
            /*var configuracao = Helper.GetConfiguracoes();
            var linkOCR = configuracao["OCR_ENDPOINT"];
            var linkGPT = configuracao["GPT_ENDPOINT"];
            var servicoOCR = checkServico(linkOCR);
            var servicoGPT = checkServico(linkGPT);
            return servicoOCR && servicoGPT;*/
            return true;
        }

        private static bool checkServico(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 5000; // Tempo limite em milissegundos (aqui está definido como 5 segundos)

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            
        }
    }
}
