using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplifAI.Services;
using System.Reflection;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Security.Cryptography;
using SimplifAI.ViewModels;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;
using SimplifAI.Models;
using SimplifAI.Utils;
using SkiaSharp;
using Microsoft.Maui.Graphics;
using System.Runtime.CompilerServices;



namespace SimplifAI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        private bool loading;
        private ScanViewModel _scanViewModel;
        public Resultado _resultado = Resultado.GetInstance();
        public ScanPage()
        {
            InitializeComponent();
            this._scanViewModel = new ScanViewModel();
            BindingContext = _scanViewModel;
        }

        private async void BtnSimplifAI_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoadingPage());
            //Loading();
            testeOCR();
            simplifAI();
            await Navigation.PopModalAsync();
            //Loading();
            await Navigation.PushAsync(new ViewModels.TextoSimplificado());

        }


        private void testeOCR()
        {
            _resultado.TextoOriginal = string.Empty;
            foreach (var arquivo in _scanViewModel.ListaAquivos)
            {
                _resultado.TextoOriginal += OCRService.LeImagem(arquivo.Caminho);
            }

        }
        private void simplifAI()
        {
            _resultado.TextoSimpliicado = GPTService.EnviaTexto(_resultado.TextoOriginal);
            _resultado.MetricaGeral = TextoHelper.CalculaMetricaGeral(_resultado.TextoOriginal);
        }

        //private void Loading()
        //{
            
        //    if (loading)
        //    {
        //        BV_Loading.IsVisible = false;
        //        AI_Rodinha.IsVisible = false;
        //        AI_Rodinha.IsRunning = false;
        //    } else
        //    {

        //        BV_Loading.IsVisible = true;
        //        AI_Rodinha.IsVisible = true;
        //        AI_Rodinha.IsRunning = true;
        //    }
        //    loading = !loading;
        //}

    }
}