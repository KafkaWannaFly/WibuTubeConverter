using WibuTubeConverter.Pages;
using WibuTubeConverter.ViewModels;

namespace WibuTubeConverter.Extensions
{
    internal static class MauiAppBuilderExtension
    {
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            var service = mauiAppBuilder.Services;

            service.AddSingleton<MainPage>()
                .AddSingleton<MainPageViewModel>()
                .AddSingleton<WibuTube>()
                .AddSingleton(FileSystem.Current);

            WibuTube.FfmpegBinaryFolder = $"{FileSystem.Current.AppDataDirectory}/Ffmpeg";

            return mauiAppBuilder;
        }
    }
}
