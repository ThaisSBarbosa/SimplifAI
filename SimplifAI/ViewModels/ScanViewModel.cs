using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Plugin.Media;
using SimplifAI.Models;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;


namespace SimplifAI.ViewModels
{
    public partial class ScanViewModel : BaseViewModel
    {
        private static string pastaAndroid = "/data/user/0/com.smartveredict.simplifai/files/TesteFoto/";
        public ScanViewModel()
        {
            listaAquivos = new ObservableCollection<Arquivo>(); ;
            Title = "Leitura";
            ExcluirItemCommand = new Command<Arquivo>(ExcluirArquivo);
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


        [RelayCommand]
        private async void capturaFoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                var stream = photo.OpenReadAsync().Result;
                byte[] imageData;
                
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    imageData = ms.ToArray();
                }
                
                if (!File.Exists(pastaAndroid))
                {
                    Directory.CreateDirectory(pastaAndroid);
                }
                var nomeFoto = Guid.NewGuid() + "_foto.jpg";
                var newFile = Path.Combine(pastaAndroid, nomeFoto);
                using (var stream2 = new MemoryStream(imageData))
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream2.CopyToAsync(newStream);
                    var a = new Arquivo();
                    a.caminho = newFile;
                    ListaAquivos.Add(a);                    
                }
            }
            catch(Exception e)
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
                IsVisible = true;
                foreach (Arquivo a in ListaAquivos)
                    a.Seleciona = true;
            }
            else
            {
                CurrentImageSource = "edit_img.png";
                IsVisible = false;
                foreach (Arquivo a in ListaAquivos)
                    a.Seleciona = false;
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

        public void apagaConteudoPasta()
        {
            var arquivos = Directory.GetFiles(pastaAndroid);

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

        #region OLD
        private List<Arquivo> GetArquivos()
        {
            var listaCaminhos = buscaArquivosAndroid();
            var listaArquivos = new List<Arquivo>();
            foreach (var foto in listaCaminhos)
            {
                if (foto != null)
                    listaArquivos.Add(new Arquivo { caminho = foto });
            }
            return listaArquivos;
        }
        public List<string> buscaArquivosAndroid()
        {
            try
            {
                return Directory.GetFiles(pastaAndroid, "*.jpeg")
                         .Union(Directory.GetFiles(pastaAndroid, "*.jpg"))
                         .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Buscar arquivos no Android apresentou o seguinte erro:");
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
    }
}