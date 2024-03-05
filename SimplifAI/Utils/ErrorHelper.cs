using ABI.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimplifAI.Utils
{
    internal class ErrorHelper
    {

        public static bool checkAcessoServicos()
        {
            var configuracao = Helper.GetConfiguracoes();
            var linkOCR = configuracao["OCR_ENDPOINT"];
            var linkGPT = configuracao["GPT_ENDPOINT"];
            var internetDispositivo = Connectivity.NetworkAccess == NetworkAccess.Internet;
            var servicoOCR = checkServico(linkOCR);
            var servicoGPT = checkServico(linkGPT);
            return internetDispositivo && servicoOCR && servicoGPT;
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
