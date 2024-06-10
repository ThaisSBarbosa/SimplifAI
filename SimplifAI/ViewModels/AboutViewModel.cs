using System.Windows.Input;

namespace SimplifAI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public double ScrollViewRowHeight { get; set; }

        public AboutViewModel()
        {
            Title = "Sobre nós";


            var screenHeight = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
            ScrollViewRowHeight = screenHeight - 200;
        }


    }
}