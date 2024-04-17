using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplifAI.Models
{
    public class Resultado
    {
        private static Resultado _instancia ; 
        private static readonly object _lock = new object();

        private string _textoOriginal;
        private string _textoProcessado;
        private string _textoSimpliicado;
        private double _metricaFK;
        private double _metricaGF;
        private double _metricaARI;
        private double _metricaCL;
        private double _metricaGeral;

        public string TextoOriginal { get { return _textoOriginal; } set { _textoOriginal = value; } }
        public string TextoProcessado { get { return _textoProcessado; } set { _textoProcessado = value; } }
        public string TextoSimplificado { get { return _textoSimpliicado; } set { _textoSimpliicado = value; } }
        public double MetricaGeral { get { return _metricaGeral; } set { _metricaGeral = value; } }

        public static Resultado GetInstance()
        {
            if (_instancia == null)
            {
                lock (_lock)
                {
                    if (_instancia == null)
                    {
                        _instancia = new Resultado();
                    }
                }
            }
            return _instancia;
        }
    }
}
