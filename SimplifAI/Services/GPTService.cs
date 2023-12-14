//using SimplifAI.Utils;
//using System;
//using System.Threading.Tasks;
//using Teste_API_GPT;

//namespace SimplifAI.Services
//{
//    public static class GPTService
//    {
//        public static async Task<string> EnviaTexto(string msg)
//        {
//            var configuracao = Helper.GetConfiguracoes();

//            IOpenAIProxy chatOpenAI = new OpenAIProxy(
//                apiKey: configuracao["GPT_API_KEY"],
//                organizationId: configuracao["GPT_ORG_ID"]);

//            var results = await chatOpenAI.SendChatMessage(msg);

//            string resultado = string.Empty;
//            foreach (var item in results)
//                resultado += $"{item.Role}: {item.Content}";

//            return resultado;
//        }
//    }
//}
