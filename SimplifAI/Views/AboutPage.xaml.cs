namespace SimplifAI.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
           
        }

        private async void BtnTermos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermsPage());
        }
    }
}