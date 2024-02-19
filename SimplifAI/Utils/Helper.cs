using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SimplifAI.Utils
{
    public static class Helper
    {
        public static Stream RetornaStreamRecursoIncorporado(string nomeRecurso)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string nomeCompletoRecurso = $"{assembly.GetName().Name}.{nomeRecurso}";
            return assembly.GetManifestResourceStream(nomeCompletoRecurso);
        }

        public static Dictionary<string, string> GetConfiguracoes()
        {
            using (Stream stream = Helper.RetornaStreamRecursoIncorporado("appSettings.json"))
            {
                if (stream == null)
                    return null;

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
            }
        }

        public static Stream RetornaStreamFoto(string caminhoFoto)
        {
            if (!File.Exists(caminhoFoto))
            {
                throw new FileNotFoundException("O arquivo de foto não foi encontrado.", caminhoFoto);
            }

            // Abre o arquivo de foto e retorna um objeto Stream
            return File.OpenRead(caminhoFoto);
        }
    }
}
