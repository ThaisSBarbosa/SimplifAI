using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;

[assembly: ExportFont("WatermelonSugar.ttf", Alias = "Font_WatermelonSugar")]
[assembly: ExportFont("Montserrat-Black.ttf", Alias = "Font_Montserrat-Black")]
[assembly: ExportFont("Montserrat-BlackItalic.ttf", Alias = "Font_Montserrat-BlackItalic")]
[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "Font_Montserrat-Bold")]
[assembly: ExportFont("Montserrat-BoldItalic.ttf", Alias = "Font_Montserrat-BoldItalic")]
[assembly: ExportFont("Montserrat-ExtraBold.ttf", Alias = "Font_Montserrat-ExtraBold")]
[assembly: ExportFont("Montserrat-ExtraBoldItalic.ttf", Alias = "Font_Montserrat-ExtraBoldItalic")]
[assembly: ExportFont("Montserrat-ExtraLight.ttf", Alias = "Font_Montserrat-ExtraLight")]
[assembly: ExportFont("Montserrat-ExtraLightItalic.ttf", Alias = "Font_Montserrat-ExtraLightItalic")]
[assembly: ExportFont("Montserrat-Italic.ttf", Alias = "Font_Montserrat-Italic")]
[assembly: ExportFont("Montserrat-Light.ttf", Alias = "Font_Montserrat-Light")]
[assembly: ExportFont("Montserrat-LightItalic.ttf", Alias = "Font_Montserrat-LightItalic")]
[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Font_Montserrat-Medium")]
[assembly: ExportFont("Montserrat-MediumItalic.ttf", Alias = "Font_Montserrat-MediumItalic")]
[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Font_Montserrat-Regular")]
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Font_Montserrat-SemiBold")]
[assembly: ExportFont("Montserrat-SemiBoldItalic.ttf", Alias = "Font_Montserrat-SemiBoldItalic")]
[assembly: ExportFont("Montserrat-Thin.ttf", Alias = "Font_Montserrat-Thin")]
[assembly: ExportFont("Montserrat-ThinItalic.ttf", Alias = "Font_Montserrat-ThinItalic")]


namespace SimplifAI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    
                    fonts.AddFont("WatermelonSugar.ttf", "Font_WatermelonSugar");
                    fonts.AddFont("Montserrat-Black.ttf", "Font_Montserrat-Black");
                    fonts.AddFont("Montserrat-BlackItalic.ttf", "Font_Montserrat-BlackItalic");
                    fonts.AddFont("Montserrat-Bold.ttf", "Font_Montserrat-Bold");
                    fonts.AddFont("Montserrat-BoldItalic.ttf", "Font_Montserrat-BoldItalic");
                    fonts.AddFont("Montserrat-ExtraBold.ttf", "Font_Montserrat-ExtraBold");
                    fonts.AddFont("Montserrat-ExtraBoldItalic.ttf", "Font_Montserrat-ExtraBoldItalic");
                    fonts.AddFont("Montserrat-ExtraLight.ttf", "Font_Montserrat-ExtraLight");
                    fonts.AddFont("Montserrat-ExtraLightItalic.ttf", "Font_Montserrat-ExtraLightItalic");
                    fonts.AddFont("Montserrat-Italic.ttf", "Font_Montserrat-Italic");
                    fonts.AddFont("Montserrat-Light.ttf", "Font_Montserrat-Light");
                    fonts.AddFont("Montserrat-LightItalic.ttf", "Font_Montserrat-LightItalic");
                    fonts.AddFont("Montserrat-Medium.ttf", "Font_Montserrat-Medium");
                    fonts.AddFont("Montserrat-MediumItalic.ttf", "Font_Montserrat-MediumItalic");
                    fonts.AddFont("Montserrat-Regular.ttf", "Font_Montserrat-Regular");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "Font_Montserrat-SemiBold");
                    fonts.AddFont("Montserrat-SemiBoldItalic.ttf", "Font_Montserrat-SemiBoldItalic");
                    fonts.AddFont("Montserrat-Thin.ttf", "Font_Montserrat-Thin");
                    fonts.AddFont("Montserrat-ThinItalic.ttf", "Font_Montserrat-ThinItalic");
                    

                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
