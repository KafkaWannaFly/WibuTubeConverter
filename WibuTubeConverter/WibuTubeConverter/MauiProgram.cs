using CommunityToolkit.Maui;
using WibuTubeConverter.Pages;
using WibuTubeConverter.ViewModels;

namespace WibuTubeConverter;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		var service = builder.Services;
		service.AddSingleton<MainPage>()
			.AddSingleton<MainPageViewModel>()
			.AddTransient<ConvertPage>()
			.AddTransient<ConvertPageViewModel>()
			.AddSingleton<WibuTube>()
			.AddSingleton(FileSystem.Current);

		return builder.Build();
	}
}
