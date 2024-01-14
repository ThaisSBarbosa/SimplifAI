using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplifAI.ViewModels;

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

        }
    }
}