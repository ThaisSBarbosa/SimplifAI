using CommunityToolkit.Mvvm.Input;
using SimplifAI.Models;
using SimplifAI.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;



namespace SimplifAI.ViewModels
{
    public partial class ScanViewModel : BaseViewModel
    {
        private static string pastaAndroid;
        public ScanViewModel()
        {
            //ApagaConteudoPasta();
            listaAquivos = new ObservableCollection<Arquivo>(); ;
            //var a = new Arquivo();
            Assembly assembly = Assembly.GetExecutingAssembly();
            //string nomeRecurso = "Resources.Images.tests.teste.png";
            //a.Caminho = $"{assembly.GetName().Name}.{nomeRecurso}";
           // a.Caminho = "teste.png";

            //listaAquivos.Add(a);
            Title = "Leitura";
            ExcluirItemCommand = new Command<Arquivo>(ExcluirArquivo);
            ModoSelecao = SelectionMode.None;
            pastaAndroid = FileHelper.CriaDirTemp();
        }

        private ObservableCollection<Arquivo> listaAquivos;
        public ObservableCollection<Arquivo> ListaAquivos
        {
            get => listaAquivos;
            set
            {
                listaAquivos = value;
                OnPropertyChanged(nameof(ListaAquivos));
                OnPropertyChanged(nameof(IsBotaoVisivel));

            }
        }
        public bool IsBotaoVisivel => ListaAquivos?.Count < 2;

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
                var media = await Microsoft.Maui.Media.MediaPicker.CapturePhotoAsync();
                var streamMedia = media.OpenReadAsync().Result;
                var foto = await ImageHelper.RetornoEmBytes(streamMedia);
                var a = new Arquivo();
                a.Caminho = await FileHelper.GuardaArquivoNoDir(foto, "_foto.jpg", pastaAndroid);
                ListaAquivos.Add(a);
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
                FileHelper.ApagaConteudoPasta(pastaAndroid);
            }
            else
            {
                return;
            }
        }


        #endregion


    }
}