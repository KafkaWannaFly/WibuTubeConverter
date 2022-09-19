using WibuTubeConverter.ViewModels;

namespace WibuTubeConverter.Pages;

public partial class ConvertPage : ContentPage
{
	public ConvertPage(ConvertViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}