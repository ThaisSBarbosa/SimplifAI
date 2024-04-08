using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace SimplifAI.Utils
{
    public static class PromptHelper
    {
        public static string retornaPrompt()
        {
            string texto = @"\n O texto acima está escrito em português e trata-se de um contrato.
                            Você deverá simplificá-lo de forma que uma pessoa leiga consiga entendê-lo com o mínimo de dificuldades, ou seja, deverá conter palavras mais simples e que são mais utilizadas no dia a dia. Você deve inclusive manter a quantidade de parágrafos e números de itens, caso houver.
                            Caso não identifique que o texto fornecido seja realmente um contrato, por favor retorne o seguinte texto: Não foi possível identificar o texto fornecido!
                            A resposta deverá sempre ser em português, de preferência o português do Brasil.                
            ";

            return texto;
        }
        


    }
}
