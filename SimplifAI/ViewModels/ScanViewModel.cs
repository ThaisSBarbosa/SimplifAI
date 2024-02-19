using CommunityToolkit.Mvvm.Input;
using SimplifAI.Models;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;



namespace SimplifAI.ViewModels
{
    public partial class ScanViewModel : BaseViewModel
    {
        private static string pastaAndroid;

        private static string criaDirTemp()
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
                Console.WriteLine($"Erro ao criar pasta temporária: {ex.Message}");
                return null;
            }
        }




        public ScanViewModel()
        {
            //apagaConteudoPasta();
            listaAquivos = new ObservableCollection<Arquivo>(); ;

            Title = "Leitura";
            ExcluirItemCommand = new Command<Arquivo>(ExcluirArquivo);
            ModoSelecao = SelectionMode.None;
            pastaAndroid = criaDirTemp();
        }

        private ObservableCollection<Arquivo> listaAquivos;
        public ObservableCollection<Arquivo> ListaAquivos
        {
            get => listaAquivos;
            set
            {
                listaAquivos = value;
                OnPropertyChanged(nameof(ListaAquivos));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;



        private bool isVisible = false;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible == value) return;
                isVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
            }
        }



        public ICommand SelecionaCommand { get; private set; }
        public ICommand ChangeImageCommand => new Command(editaArquivos);

        private string currentImageSource = "edit_img.png";
        public ICommand ExcluirItemCommand { get; private set; }
        public string CurrentImageSource
        {
            get { return currentImageSource; }
            set
            {
                if (currentImageSource != value)
                {
                    currentImageSource = value;
                    OnPropertyChanged(nameof(CurrentImageSource));
                }
            }
        }

        private async Task<Stream> CompressImage(byte[] originalStream)
        {
            try
            {
                // Carregar a imagem original
                using (var originalBitmap = SKBitmap.Decode(originalStream))
                {
                    var originalBitmapRotated = Rotate(originalStream);
                    var width = originalBitmapRotated.Width;
                    var height = originalBitmapRotated.Height;

                    // Redimensionar a imagem para uma largura máxima de 100 pixels
                    //var resizedBitmap = originalBitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
                    var resizedBitmap = originalBitmapRotated.Resize(new SKImageInfo(width, height), SKFilterQuality.High);

                    // Converter o bitmap redimensionado de volta para um stream
                    var compressedStream = new MemoryStream();
                    resizedBitmap.Encode(compressedStream, SKEncodedImageFormat.Jpeg, 100);

                    // Reiniciar a posição do stream para o início
                    compressedStream.Position = 0;

                    return compressedStream;
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, se houver
                return null;
            }
            finally
            {
                //originalStream?.Dispose();
            }
        }

        public static SKBitmap Rotate(byte[] photo)
        {
            using (var bitmap = SKBitmap.Decode(photo))
            {


                var rotated = new SKBitmap(bitmap.Height, bitmap.Width);

                using (var surface = new SKCanvas(rotated))
                {
                    surface.Translate(rotated.Width, 0);
                    surface.RotateDegrees(90);
                    surface.DrawBitmap(bitmap, 0, 0);
                }

                return rotated;
            }
        }

        [RelayCommand]
        private async void capturaFoto()
        {
            try
            {
                var photo = await Microsoft.Maui.Media.MediaPicker.CapturePhotoAsync();
                var stream = photo.OpenReadAsync().Result;
                byte[] imageData;
                byte[] imageMenorData;

                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    imageData = ms.ToArray();
                }

                // ######## resize? 

                var imagemMenor = await CompressImage(imageData);


                using (var ms = new MemoryStream())
                {
                    imagemMenor.CopyTo(ms);
                    imageMenorData = ms.ToArray();
                }



                // #############


                if (!File.Exists(pastaAndroid))
                {
                    System.IO.Directory.CreateDirectory(pastaAndroid);
                }
                var nomeFoto = Guid.NewGuid() + "_foto.jpg";
                var newFile = Path.Combine(pastaAndroid, nomeFoto);
                using (var stream2 = new MemoryStream(imageMenorData))
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream2.CopyToAsync(newStream);
                    var a = new Arquivo();
                    a.Caminho = newFile;
                    ListaAquivos.Add(a);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao capturar foto: " + e.Message);
            }

        }

        [RelayCommand]
        private async void editaArquivos()
        {
            if (CurrentImageSource == "edit_img.png")
            {
                CurrentImageSource = "edit_img_edit.png";
                ModoSelecao = SelectionMode.Multiple;
                IsVisible = true;
            }
            else
            {
                CurrentImageSource = "edit_img.png";
                ModoSelecao = SelectionMode.None;
                IsVisible = false;
            }


        }

        private SelectionMode _modoSelecao;
        public SelectionMode ModoSelecao
        {
            get => _modoSelecao;
            set
            {
                if (_modoSelecao != value)
                {
                    _modoSelecao = value;
                    OnPropertyChanged(nameof(ModoSelecao)); // Notifica a alteração da propriedade
                }
            }
        }

        private void ExcluirArquivo(Arquivo arquivo)
        {
            ListaAquivos.Remove(arquivo);
        }


        #region COMANDO APAGA TUDO
        private Command confirmaExclusaoTotalCommand;
        public Command ConfirmaExclusaoTotalCommand => confirmaExclusaoTotalCommand ??= new Command(async () => await mostraMsgConfirmaExclusao());

        private async Task mostraMsgConfirmaExclusao()
        {
            bool resposta = await Application.Current.MainPage.DisplayAlert("Confirmação", "Tem certeza que deseja apagar todas os arquivos?", "OK", "CANCEL");

            if (resposta)
            {
                ListaAquivos.Clear();
                apagaConteudoPasta();
            }
            else
            {
                return;
            }
        }

        public static void apagaConteudoPasta()
        {
            var arquivos = System.IO.Directory.GetFiles(pastaAndroid);

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

        #endregion


    }
}