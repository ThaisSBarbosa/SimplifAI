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
            //testeOCR();
            //SimplifAI();

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
                SimplifAI();
                await Navigation.PopModalAsync();
                //Loading();
                await Navigation.PushAsync(new ViewModels.TextoSimplificado());
            } 
            else
            {
                 await DisplayAlert("Erro","Não há fotos na lista!","Ok");            
            }
        }

        private void testeOCR()
        {
            _resultado.TextoOriginal = string.Empty;
            foreach (var arquivo in _scanViewModel.ListaAquivos)
            {
                _resultado.TextoOriginal += OCRService.LeDocumento(arquivo.Caminho);
            }

        }

        private async void SimplifAI()
        {
            //_resultado.TextoOriginal = "\r\nNa esfera jurídica, a arrematação é o ato pelo qual um bem é adjudicado a um terceiro " +
            //    "mediante leilão judicial, em decorrência de uma execução fiscal. Ao arrazoar sobre o processo, o advogado " +
            //    "utiliza sua argumentação para defender os interesses do cliente perante o juízo. Posteriormente, o advogado " +
            //    "pode ajuizar uma ação com vistas a salvaguardar os direitos do cliente, seguindo os trâmites processuais cabíveis " +
            //    "conforme o ordenamento jurídico vigente.";

            var termosDefinicoes = await Neo4jService.RetornaTermosDefinicoes();            
            _resultado.TextoProcessado = ProcessamentoTextoService.AdicionaDefinicoesNeo4j(_resultado.TextoOriginal, termosDefinicoes);
            var textoCompleto = _resultado.TextoProcessado + PromptHelper.retornaPrompt();
            _resultado.TextoSimplificado = GPTService.EnviaTexto(textoCompleto);
            _resultado.MetricaGeral = TextHelper.CalculaMetricaGeral(_resultado.TextoOriginal);
        }
    }
}