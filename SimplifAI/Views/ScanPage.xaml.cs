using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplifAI.Services;
using System.Reflection;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Security.Cryptography;
using SimplifAI.ViewModels;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

namespace SimplifAI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {                       
            InitializeComponent();
        }

        private void BtnSimplifAI_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewModels.TextoConvertido());
        }
    }


}