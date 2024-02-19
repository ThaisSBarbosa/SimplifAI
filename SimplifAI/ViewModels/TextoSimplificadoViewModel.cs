using SimplifAI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SimplifAI.ViewModels
{
	public class TextoSimplificadoViewModel : BaseViewModel
	{
        public Resultado _resultado = Resultado.GetInstance();

        private string _textoOriginal;

		public string TextoOriginal
		{ 
			get => _textoOriginal;
		
		set
			{
                if (_textoOriginal != value)
                {
                    _textoOriginal = value;
                    OnPropertyChanged();
                }
            }
		}

		private string _textoSimplificado;

        public string TextoSimplificado
        {
            get => _textoSimplificado;

            set
            {
                if (_textoSimplificado != value)
                {
                    _textoSimplificado = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _metricaGeral;

        public double MetricaGeral
        {
            get => _metricaGeral;

            set
            {
                if (_metricaGeral != value)
                {
                    _metricaGeral = value;
                    OnPropertyChanged();
                }
            }
        }

        public TextoSimplificadoViewModel ()
		{
            Title = "Texto simplificado";
			TextoOriginal = _resultado.TextoOriginal;
            TextoSimplificado = _resultado.TextoSimpliicado;
            MetricaGeral = _resultado.MetricaGeral;
        }



	}
}