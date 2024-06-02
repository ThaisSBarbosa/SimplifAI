using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimplifAI.Utils
{
    internal static class TextHelper
    {
        //private double _metricaFK;
        //private double _metricaGF;

        private static int CalculaNroPalavras(string texto)
        {
            string[] palavras = texto.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return palavras.Length;
        }
       
        private static int CalculaNroLetras(string texto)
        {
            int count = 0;

            foreach (char c in texto)
            {
                // Verifica se o caractere é uma letra do alfabeto
                if (char.IsLetter(c))
                {
                    count++;
                }
            }

            return count;
        }
        
        private static int CalculaNroFrases(string texto)
        {
            string limitadores = @"[.!?]+";
            string[] frases = Regex.Split(texto, limitadores);
            int numeroFrases = 0;
            foreach (string frase in frases)
            {
                if (!string.IsNullOrWhiteSpace(frase))
                {
                    numeroFrases++;
                }
            }
            return numeroFrases;
        }

        public static double CalculaMetricaARI(string texto)
        {            
            int nroLetras = CalculaNroLetras(texto);
            int nroPalavras = CalculaNroPalavras(texto);
            int nroFrases = CalculaNroFrases(texto);

            double ARI = (0.44 * (nroPalavras / (double)nroFrases)) + ((4.6 * (nroLetras / (double)nroPalavras))) - 20;

            return ARI;
        }

        public static double CalculaMetricaCL(string texto)
        {            
            int nroLetras = CalculaNroLetras(texto);
            int nroPalavras = CalculaNroPalavras(texto);
            int nroFrases = CalculaNroFrases(texto);

            double CL = (5.4 * (nroLetras / (double)nroPalavras)) - (21 * (nroFrases / (double)nroPalavras)) - 14;

            return CL;
        }

        public static double CalculaMetricaGeral(string texto)
        {
            return ((CalculaMetricaARI(texto) + CalculaMetricaCL(texto)) / 2);

        }

        public static string RetiraTextosExtras(string texto)
        {
            string[] stringsParaExcluir = [ ":unselected:", ":selected:"];

            foreach (string s in stringsParaExcluir)
            {

                //while (texto.IndexOf(s)>=0)    
                  //  texto = texto.Remove(texto.IndexOf(s),s.Length);
            }
            return texto;
        }
    }
}
