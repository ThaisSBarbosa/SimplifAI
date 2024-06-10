namespace SimplifAI.Views
{
    public partial class TermsPage : ContentPage
    {
        public TermsPage()
        {
            InitializeComponent();
        }

        private async void btnVolta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}