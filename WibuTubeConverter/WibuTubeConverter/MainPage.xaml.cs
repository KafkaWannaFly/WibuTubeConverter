using CommunityToolkit.Maui.Views;
using WibuTubeConverter.Controls;
using WibuTubeConverter.ViewModels;

namespace WibuTubeConverter;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

