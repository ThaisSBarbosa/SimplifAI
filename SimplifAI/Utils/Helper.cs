using Newtonsoft.Json;
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


    }
}
