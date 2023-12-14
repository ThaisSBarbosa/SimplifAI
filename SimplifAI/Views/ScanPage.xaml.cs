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

namespace SimplifAI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
        }

        private void BtnSimplifAI_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewModels.TextoConvertido());
        }

        private async void BtnCapturarCamera_Clicked(object sender, EventArgs e)
        {
            //var pocOCR = await OCRService.LeImagem();

            //var respostaGPT = await GPTService.EnviaTexto("Quem é você?");

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                // A câmera não está disponível ou a captura de foto não é suportada

                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            // 'file' agora contém a foto capturada
            // Você pode manipular o arquivo conforme necessário
            string filePath = file.Path;
        }
    }
}