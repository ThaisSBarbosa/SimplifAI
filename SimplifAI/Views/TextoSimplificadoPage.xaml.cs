
namespace SimplifAI.ViewModels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TextoSimplificado : ContentPage
	{
        public TextoSimplificado ()
		{
            InitializeComponent();
        }

        private void btnExpandOriginal_Clicked(object sender, EventArgs e)
        {
            TextoOriginal.IsVisible = !TextoOriginal.IsVisible;
            if (TextoOriginal.IsVisible == true)
                btnExpandOriginal.Source = "arrow_down.png";
            else
                btnExpandOriginal.Source = "arrow_right.png";
            
        }

        private void ExpandeSimplificado(object sender, EventArgs e)
        {
            TextoSimples.IsVisible = !TextoSimples.IsVisible;
            if (TextoSimples.IsVisible == true)
                btnExpandSimplificado.Source = "arrow_down.png";
            else
                btnExpandSimplificado.Source = "arrow_right.png";
        }
    }
}