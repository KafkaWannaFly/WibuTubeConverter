using WibuTubeConverter.ViewModels;

namespace WibuTubeConverter.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

