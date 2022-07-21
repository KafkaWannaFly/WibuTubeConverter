using WibuTubeConverter.ViewModels;

namespace WibuTubeConverter.Pages;

[QueryProperty(nameof(VideoFileInfo), "videoFileInfo")]
public partial class ConvertPage : ContentPage
{
	public FileInfo VideoFileInfo { get; set; }

	public ConvertPage(ConvertPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}