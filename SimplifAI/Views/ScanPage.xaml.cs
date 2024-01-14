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




namespace SimplifAI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {

        List<Foto> fotos { get; set; }

        

        private bool HasPhoto;

        private string photoPath;
        private string CompletePhotoPath
        {  
            get => photoPath; 
            set { 
                photoPath = value;
                HasPhoto = !string.IsNullOrEmpty(value);
            }}
        public ScanPage()
        {
            
            fotos = GetFotos();
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
            
            

            try
            {
              
                var photo = await MediaPicker.CapturePhotoAsync();
                // Rest of your code...

                if (photo == null)
                {
                    DisplayAlert("Erro Captura", "foto vazia", "Ok");
                    return;
                }

                var stream = photo.OpenReadAsync().Result;

                byte[] imageData;

                using(MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    imageData = ms.ToArray(); 
                }

                var width = 300;
                var height = 300;

                var folderPath = Path.Combine(FileSystem.AppDataDirectory, "TesteFoto");
                if(!File.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var nomeFoto = Guid.NewGuid() + "_foto.jpg";

                var newFile = Path.Combine(folderPath, nomeFoto);
                using (var strean2 = new MemoryStream(imageData))
                using (var newStream = File.OpenWrite(newFile)) 
                {
                    await strean2.CopyToAsync(newStream);
                    CompletePhotoPath = newFile;
                    Image ImagemCapturada = new Image();
                    ImagemCapturada.Source = CompletePhotoPath;
                    fotos = GetFotos();
                    fotos.Add(new Foto { CaminhoDaFoto = CompletePhotoPath });
                }

         


                    //fotos.Add();
                    DisplayAlert("Captura", "foto salva", "Ok");




            }
            catch (Exception ex)
            {
                // Handle exceptions...
                DisplayAlert("Deu ruim", ex.Message, "Ok");
            }



            /*
             
            
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
            */


        }

        private List<Foto> GetFotos()
        {
            // Implemente este método para obter e retornar sua coleção real de fotos
            // Exemplo:
            var listaFotosAndroid = buscaFotosAndroid();
            var fotos = new List<Foto>();
            foreach (var foto in listaFotosAndroid)
            {
                fotos.Add(new Foto { CaminhoDaFoto =  foto });
            }
            return fotos;
        }

        public List<string> buscaFotosAndroid()
        {
            try
            {
                string pastaAndroid = "/data/user/0/com.smartveredict.simplifai/files/TesteFoto/";
                return Directory.GetFiles(pastaAndroid, "*.jpeg")
                         .Union(Directory.GetFiles(pastaAndroid, "*.jpg"))
                         .ToList();
            } catch (Exception ex)
            {
                DisplayAlert("Deu ruim","ruim ao pegar pasta do android","OK");
                return null;
            }
        }

    }



    public class Foto
    {
        public string CaminhoDaFoto { get; set; }


    }
}