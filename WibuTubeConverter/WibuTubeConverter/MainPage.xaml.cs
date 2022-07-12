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

    private void Button_Clicked(object sender, EventArgs e)
    {
        var popUp = new LoadingPopUp();
        this.ShowPopup(popUp);
    }
}

