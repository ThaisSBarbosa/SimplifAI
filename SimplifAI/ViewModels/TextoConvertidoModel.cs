using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplifAI.ViewModels
{
	public class TextoConvertidoModel : ContentPage
	{
		public TextoConvertidoModel ()
		{
            Title = "Texto convertido";
            Content = new StackLayout {
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
		}
	}
}