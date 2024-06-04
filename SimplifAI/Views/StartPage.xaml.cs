using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace SimplifAI.Views
{
    public partial class StartPage : ContentPage
    {
        private ObservableCollection<string> _gifs;
        public StartPage()
        {
            InitializeComponent();

            carousel.IndicatorView = indicatorView;
            _gifs = new ObservableCollection<string>
        {
            "start2.gif",
            "middle.gif",
            "end.gif"
            };

            carousel.ItemsSource = _gifs;
        }

        private void OnCarouselViewPositionChanged(object sender, PositionChangedEventArgs e)
        {
            // Verifica se a posição atual é a última página
            var mostraIcones = e.CurrentPosition == _gifs.Count - 1;
            LoginButton.IsEnabled = mostraIcones;
            LblAceiteTermos.IsVisible = mostraIcones;
            AceiteTermos.IsVisible = mostraIcones;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Navega para a página de login ou realiza a ação de login
            await DisplayAlert("Login", "Botão de Login Clicado", "OK");
            // Aqui você pode adicionar a navegação para a página de login
            // Exemplo: await Navigation.PushAsync(new LoginPage());
        }
    }
}