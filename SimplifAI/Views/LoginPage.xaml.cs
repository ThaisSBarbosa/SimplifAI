
using SimplifAI.Services;

namespace SimplifAI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            // this.BindingContext = new LoginViewModel();
        }

        private void BTN_SignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtUserName.Text) || string.IsNullOrEmpty(TxtUserPass.Text))
                    throw new Exception("Preencha usuário e senha!");
                if (!ChBxCheckTerms.IsChecked)
                    throw new Exception("Aceite os termos de uso!");

                MySqlService.Insere(TxtUserName.Text, TxtUserPass.Text);
                DisplayAlert("Atenção", "Cadastro realizado", "OK");
                Navigation.PushAsync(new Views.UserPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Atenção", ex.Message, "OK");
            }
        }
    }
}