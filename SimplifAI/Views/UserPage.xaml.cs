
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

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(TxtUserName.Text) || string.IsNullOrEmpty(TxtSenha.Text))
                    throw new Exception("Preencha usuário e senha!");

                bool usuarioValido = MySqlService.ValidaUsuario(TxtUserName.Text, TxtSenha.Text);

                if (!usuarioValido)
                    throw new Exception("Usuário ou senha inválidos!");

                Navigation.PushAsync(new Views.ScanPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Atenção", ex.Message, "OK");
            }
        }

        private void BTN_PrimeiroAcesso_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.LoginPage());
        }
    }
}