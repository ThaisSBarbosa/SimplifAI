using SimplifAI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimplifAI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorPage : ContentPage
    {
        
        public ErrorPage(string texto, string imagem)
        {
            
            InitializeComponent();
            imagemErro.Source = imagem;
            textoErro.Text = texto;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                // Lógica a ser executada quando ocorrer um toque no StackLayout
                // Por exemplo, exibir uma mensagem
                DisplayAlert("Toque", "Você tocou no StackLayout!", "OK");
            };


        }

        async void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            // Handle the tap
            await DisplayAlert("teste","tela tocada","ok");
            await this.Navigation.PopModalAsync();
        }
    }
}