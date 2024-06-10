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

        private double _metricaTextoSimplificado;

        public double MetricaTextoSimplificado
        {
            get => _metricaTextoSimplificado;

            set
            {
                if (_metricaTextoSimplificado != value)
                {
                    _metricaTextoSimplificado = value;
                    OnPropertyChanged();
                }
            }
        }


        private double _metricaTextoOriginal;

        public double MetricaTextoOriginal
        {
            get => _metricaTextoOriginal;

            set
            {
                if (_metricaTextoOriginal != value)
                {
                    _metricaTextoOriginal = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _pbTextoOriginal;
        private double _pbTextoSimplificado;

        public double PBTextoOriginal
        {
            get => _pbTextoOriginal;

            set
            {
                if (_pbTextoOriginal != value)
                {
                    _pbTextoOriginal = value;
                    OnPropertyChanged();
                }
            }
        }

        public double PBTextoSimplificado
        {
            get => _pbTextoSimplificado;

            set
            {
                if (_pbTextoSimplificado != value)
                {
                    _pbTextoSimplificado = value;
                    OnPropertyChanged();
                }
            }
        }
        public TextoSimplificadoViewModel ()
		{
            Title = "Texto simplificado";
			TextoOriginal = _resultado.TextoOriginal;
            TextoSimplificado = _resultado.TextoSimplificado;
            MetricaGeral = _resultado.MetricaGeral;
            MetricaTextoOriginal = _resultado.MetricaTextoOriginal;
            MetricaTextoSimplificado = _resultado.MetricaTextoSimplificado;
            CalculaPB();
        }

        private void CalculaPB()
        {

            if (MetricaTextoOriginal > MetricaTextoSimplificado)
            {
                PBTextoOriginal = 1;
                PBTextoSimplificado = (MetricaTextoSimplificado / MetricaTextoOriginal);
            }
            else
            {
                PBTextoSimplificado = 1;
                PBTextoOriginal = (PBTextoOriginal / PBTextoSimplificado);
            }
        }


	}
}