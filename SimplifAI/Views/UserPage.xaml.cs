using SimplifAI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifAI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPage : ContentPage
	{
		public UserPage ()
		{
			InitializeComponent ();

            //var testeOCR = OCRService.LeImagem();
            //var testeGPT = GPTService.EnviaTexto("Simplifique explicando de maneira mais fácil este texto " +
            //    "jurídico para alguém que não possui familiaridade com o meio: " + testeOCR);
		}

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {

        }

        private void BTN_Login_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.LoginPage());
        }
    }
}