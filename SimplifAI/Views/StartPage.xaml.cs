using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using SimplifAI.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SimplifAI.Views
{
    public partial class StartPage : ContentPage
    {
        private ObservableCollection<string> _gifs;
        private StartViewModel _startViewModel;

        public StartPage()
        {
            InitializeComponent();
            this._startViewModel = new StartViewModel();
            BindingContext = _startViewModel;

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
            LblAceiteTermos.IsVisible = mostraIcones;
            AceiteTermos.IsVisible = mostraIcones;
            BtnTermos.IsVisible = mostraIcones;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Navega para a página de login ou realiza a ação de login
           // await DisplayAlert("Login", "Botão de Login Clicado", "OK");
            // Aqui você pode adicionar a navegação para a página de login
           //await Navigation.PushAsync(new AppShell());
            Application.Current.MainPage = new AppShell();
        }

        private async void BtnTermos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermsPage());
        }
    }
}