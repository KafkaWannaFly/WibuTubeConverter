using WibuTubeConverter.Pages;
using WibuTubeConverter.ViewModels;

namespace WibuTubeConverter.Extensions
{
    internal static class MauiAppBuilderExtension
    {
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            var service = mauiAppBuilder.Services;

            service.AddSingleton<MainPage>()
                .AddSingleton<MainPageViewModel>()
                .AddTransient<ConvertPage>()
                .AddTransient<ConvertViewModel>()
                .AddSingleton<WibuTube>()
                .AddSingleton(FileSystem.Current);

            WibuTube.FfmpegBinaryFolder = $"{FileSystem.Current.AppDataDirectory}/Ffmpeg";

            return mauiAppBuilder;
        }
    }
}
