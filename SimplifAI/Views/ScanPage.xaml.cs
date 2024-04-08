using SimplifAI.Services;
using SimplifAI.ViewModels;
using SimplifAI.Models;
using SimplifAI.Utils;


namespace SimplifAI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        private bool loading;
        private ScanViewModel _scanViewModel;
        public Resultado _resultado = Resultado.GetInstance();
        private string imagemSemInternet = "./no_internet.png";
        public ScanPage()
        {
            InitializeComponent();
            this._scanViewModel = new ScanViewModel();
            BindingContext = _scanViewModel;
        }

        private async void BtnSimplifAI_Clicked(object sender, EventArgs e)
        {
            if (_scanViewModel.ListaAquivos.Count > 0)
            {
                if (!ErrorHelper.checkAcessoServicos())
                {
                    //await Navigation.PopModalAsync();
                    //await Navigation.PushModalAsync(new ErrorPage("Sem Internet!", imagemSemInternet));
                    var ep = new ErrorPage2();
                    
                    await ep.OpenBottomSheet();
                    //await Navigation.PushModalAsync(new ErrorPage2());
                    //await Navigation.PopModalAsync();
                    return;
                }
                await Navigation.PushModalAsync(new LoadingPage());
               
                //Loading();
                testeOCR();
                simplifAI();
                await Navigation.PopModalAsync();
                //Loading();
                await Navigation.PushAsync(new ViewModels.TextoSimplificado());
            } else
            {
                 await DisplayAlert("Erro","Não há fotos na lista!","Ok");            }
        }


        private void testeOCR()
        {
            _resultado.TextoOriginal = string.Empty;
            foreach (var arquivo in _scanViewModel.ListaAquivos)
            {
                _resultado.TextoOriginal += OCRService.LeDocumento(arquivo.Caminho);
            }

        }
        private void simplifAI()
        {
            var textoCompleto = _resultado.TextoOriginal + PromptHelper.retornaPrompt();
            _resultado.TextoSimpliicado = GPTService.EnviaTexto(textoCompleto);
            _resultado.MetricaGeral = TextHelper.CalculaMetricaGeral(_resultado.TextoOriginal);
        }

    }
}