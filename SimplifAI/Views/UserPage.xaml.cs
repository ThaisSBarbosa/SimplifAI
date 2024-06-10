
using SimplifAI.Services;

namespace SimplifAI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();

            //var testeOCR = OCRService.LeDocumento();
            //var testeGPT = GPTService.EnviaTexto("Simplifique explicando de maneira mais fácil este texto " +
            //    "jurídico para alguém que não possui familiaridade com o meio: " + testeOCR);
        }

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoadingPage());
            try
            {
                if(string.IsNullOrEmpty(TxtUserName.Text) || string.IsNullOrEmpty(TxtSenha.Text))
                    throw new Exception("Preencha usuário e senha!");

                bool usuarioValido = MySqlService.ValidaUsuario(TxtUserName.Text, TxtSenha.Text);

                if (!usuarioValido)
                    throw new Exception("Usuário ou senha inválidos!");
                await Navigation.PopModalAsync();
                await DisplayAlert("Login", "Usuário logado com sucesso!", "OK");
                await Shell.Current.GoToAsync("//ScanPage");
            }
            catch (Exception ex)
            {
                await Navigation.PopModalAsync();
                await DisplayAlert("Atenção", ex.Message, "OK");
            }
        }

        private async void BTN_PrimeiroAcesso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.LoginPage());
        }
    }
}