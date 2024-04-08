using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace SimplifAI.Utils
{
    public static class PromptHelper
    {
        public static string msgSystem = "Você é um especialista jurídico que escreve de forma simples e acessível, facilitando o entendimento de textos legais para pessoas leigas.\n" +
            "Caso identifique que o texto fornecido não é realmente um contrato, por favor retorne o seguinte texto: \"Desculpe, mas só consigo processar e trabalhar com contratos jurídicos. " +
            "Por favor insira fotos de um contrato escaneado para iniciar a simplificação.\"";

        public static string msgPrompt = @"\n O texto acima está escrito em português e trata-se de um contrato.
            Você deverá simplificá-lo de forma que uma pessoa leiga consiga entendê-lo com o mínimo de dificuldades, ou seja, deverá conter palavras mais simples e que são mais utilizadas no dia a dia. Você deve inclusive manter a quantidade de parágrafos e números de itens, caso houver.
            Caso não identifique que o texto fornecido seja realmente um contrato, por favor retorne o seguinte texto: Não foi possível identificar o texto fornecido!
            A resposta deverá sempre ser em português, de preferência o português do Brasil.";

        public static string retornaPrompt()
        {
            return msgPrompt;
        }

        public static string retornaMsgSystem()
        {
            return msgSystem;
        }
    }
}
