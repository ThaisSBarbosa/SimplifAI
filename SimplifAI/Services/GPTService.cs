using Azure.AI.OpenAI;
using Azure;
using SimplifAI.Utils;

namespace SimplifAI.Services
{
    public static class GPTService
    {
        public static string EnviaTexto(string msg)
        {
            var configuracao = Helper.GetConfiguracoes();
            var endpoint = configuracao["GPT_ENDPOINT"];
            var key = configuracao["GPT_API_KEY"];

            OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));

            Response<ChatCompletions> responseWithoutStream = client.GetChatCompletions("deploy-gpt-simplify",
                new ChatCompletionsOptions()
                {
                    Messages =
                    {
                        new ChatMessage(ChatRole.System, PromptHelper.retornaMsgSystem()),
                        new ChatMessage(ChatRole.User, msg + PromptHelper.retornaPrompt()),
                        //new ChatMessage(ChatRole.Assistant, @"Microsoft was founded on April 4, 1975."),
                    },
                    Temperature = (float)0.7,
                    MaxTokens = 2000,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });

            ChatCompletions response = responseWithoutStream.Value;

            string resultado = string.Empty;

            if (response != null)
            {
                resultado = response.Choices[0].Message.Content;
                for (int i = 1; i<response.Choices.Count; i++)
                    resultado += "\n" + response.Choices[i].Message.Content ;
                
                    
            }

            //IOpenAIProxy chatOpenAI = new OpenAIProxy(
            //    apiKey: configuracao["GPT_API_KEY"],
            //    organizationId: configuracao["GPT_ORG_ID"]);

            //var results = await chatOpenAI.SendChatMessage(msg);

            //string resultado = string.Empty;
            //foreach (var item in results)
            //    resultado += $"{item.Role}: {item.Content}";

            return resultado;
        }
    }
}
