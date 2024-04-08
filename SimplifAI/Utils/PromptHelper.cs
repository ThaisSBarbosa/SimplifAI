using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace SimplifAI.Utils
{
    public static class PromptHelper
    {
        public static string msgSystem = "Você é um especialista jurídico que escreve de forma simples e acessível, facilitando o entendimento de textos " +
            "legais para pessoas leigas.\nCaso identifique que o texto fornecido não é realmente um contrato ou um trecho de contrato, " +
            "por favor retorne o seguinte texto: \"Desculpe, mas só consigo processar e trabalhar com contratos ou trechos de contratos jurídicos. " +
            "Por favor insira fotos de um contrato ou de trechos de contrato para iniciar a simplificação.\"";

        public static string msgPrompt = @"\n O texto acima está escrito em português e trata-se de um contrato ou de um trecho de contrato.
            Você deverá simplificá-lo de forma que uma pessoa leiga consiga entendê-lo com o mínimo de dificuldades, ou seja, deverá conter palavras 
            mais simples e que são mais utilizadas no dia a dia. Você deve inclusive manter a quantidade de parágrafos e números de itens, caso houver.
            A resposta deverá sempre ser em português, de preferência, o português do Brasil.";

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
