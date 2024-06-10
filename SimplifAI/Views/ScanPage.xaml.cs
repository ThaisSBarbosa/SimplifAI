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
            if (_scanViewModel.ListaAquivos.Count >= 0)
            {
                /*if (!ErrorHelper.checkAcessoServicos())
                {
                    //await Navigation.PopModalAsync();
                    //await Navigation.PushModalAsync(new ErrorPage("Sem Internet!", imagemSemInternet));
                    var ep = new ErrorPage2();
                    
                    await ep.OpenBottomSheet();
                    //await Navigation.PushModalAsync(new ErrorPage2());
                    //await Navigation.PopModalAsync();
                    return;
                }
               */

                //Loading();
                await Navigation.PushModalAsync(new LoadingPage());

                LeDocumento();

                await SimplifAI();
                await Navigation.PopModalAsync();
                //Loading();
                await Navigation.PushAsync(new ViewModels.TextoSimplificado());
            }
            else
            {
                await DisplayAlert("Erro", "Capture uma foto!", "Ok");
            }
        }

        private void LeDocumento()
        {
            //_resultado.TextoOriginal = @"Na esfera jurídica, a arrematação é o ato pelo qual um bem é adjudicado a um terceiro mediante leilão judicial, em decorrência de uma execução fiscal. Ao arrazoar sobre o processo, o advogado utiliza sua argumentação para defender os interesses do cliente perante o juízo. Posteriormente, o advogado pode ajuizar uma ação com vistas a salvaguardar os direitos do cliente, seguindo os trâmites processuais cabíveis conforme o ordenamento jurídico vigente.";

            //return;
            try
            {
                foreach (var arquivo in _scanViewModel.ListaAquivos)
                {
                    _resultado.TextoOriginal += OCRService.LeDocumento(arquivo.Caminho);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                DisplayAlert("Erro", "Erro ao ler documento!", "Ok");
            }

        }

        private async Task SimplifAI()
        {
            

            try
            {
                if (!_resultado.TextoOriginal.Contains("Termo") && !_resultado.TextoOriginal.Contains("termo") &&
                    !_resultado.TextoOriginal.Contains("Contrato") && !_resultado.TextoOriginal.Contains("contrato"))
                {
                    _resultado.TextoSimplificado = "Desculpe, mas só consigo processar e trabalhar com contratos jurídicos. " +
                        "Por favor insira fotos de um contrato para iniciar a simplificação.";
                }
                else
                {
                    var termosDefinicoes = await Neo4jService.RetornaTermosDefinicoes();
                    _resultado.TextoProcessado = ProcessamentoTextoService.AdicionaDefinicoesNeo4j(_resultado.TextoOriginal, termosDefinicoes);

                    var textoCompleto = _resultado.TextoProcessado + PromptHelper.retornaPrompt();
                    _resultado.TextoSimplificado = await GPTService.EnviaTexto(textoCompleto);
                }

                _resultado.MetricaTextoOriginal = TextHelper.CalculaMetricaGeral(_resultado.TextoOriginal);
                _resultado.MetricaTextoSimplificado = TextHelper.CalculaMetricaGeral(_resultado.TextoSimplificado);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await DisplayAlert("Erro", "Erro ao simplificar documento!", "Ok");
            }
        }
    }
}