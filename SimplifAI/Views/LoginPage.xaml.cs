
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

        private async void BTN_SignIn_Clicked(object sender, EventArgs e)
        {

            TxtUserPass.Unfocus();
            TxtUserName.Unfocus();
            await Navigation.PushModalAsync(new LoadingPage());
            try
            {
                if (string.IsNullOrEmpty(TxtUserName.Text) || string.IsNullOrEmpty(TxtUserPass.Text))
                    throw new Exception("Preencha usuário e senha!");
                if (!ChBxCheckTerms.IsChecked)
                    throw new Exception("Aceite os termos de uso!");

                var erro = MySqlService.Insere(TxtUserName.Text, TxtUserPass.Text);
                if (erro != -1)
                {
                    await Navigation.PopModalAsync();
                    await DisplayAlert("Atenção", "Cadastro realizado", "OK");

                    await Shell.Current.GoToAsync("//UserPage");
                }
                else
                {
                    await Navigation.PopModalAsync();
                    await DisplayAlert("Atenção", "Ocorreu um erro!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "OK");
            }
           // await Navigation.PopModalAsync();
        }
    }
}