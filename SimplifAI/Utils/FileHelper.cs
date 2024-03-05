using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifAI.Utils
{
    internal static class FileHelper
    {

        public static string CriaDirTemp()
        {
            // Cria o diretório temporário
            string pastaTemp = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.Current.AppDataDirectory, "SimplifAI Temp");

            try
            {
                System.IO.Directory.CreateDirectory(pastaTemp);
                return pastaTemp;

            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Erro ao criar pasta temporária: {ex.Message}");
                return null;
            }
        }

        public static void ApagaConteudoPasta(string caminho)
        {
            var arquivos = System.IO.Directory.GetFiles(caminho);

            try
            {
                // Itera sobre cada arquivo e o exclui
                foreach (var arquivo in arquivos)
                {
                    File.Delete(arquivo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deu erro ao deletar os arquivos da pasta. Motivo:");
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<string> GuardaArquivoNoDir(byte[] arquivo,string extensao,string caminho)
        {
            if (!File.Exists(caminho))
            {
                System.IO.Directory.CreateDirectory(caminho);
            }
            var nomeArquivo = Guid.NewGuid() + extensao;
            var nomeArquivoComCaminho = Path.Combine(caminho, nomeArquivo);
            using (var stream2 = new MemoryStream(arquivo))
            using (var newStream = File.OpenWrite(nomeArquivoComCaminho))
            {
                await stream2.CopyToAsync(newStream);                
            }
            return nomeArquivoComCaminho;
        }
    }
}
