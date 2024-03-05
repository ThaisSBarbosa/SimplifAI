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
        
        public ErrorPage(string texto)
        {

            InitializeComponent();
            textoErro.Text = texto;
        }

    }
}