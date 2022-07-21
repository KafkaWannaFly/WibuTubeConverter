using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;

namespace WibuTubeConverter.Controls
{
    public partial class LoadingPopUp : Popup
    {
        public LoadingPopUp(string title = "Downloading...")
        {
            CanBeDismissedByTappingOutsideOfPopup = false;
            Color = Colors.Transparent;
            Content = new Border
            {
                Content = new FlexLayout
                {
                    Direction = FlexDirection.Column,
                    JustifyContent = FlexJustify.SpaceBetween,
                    AlignItems = FlexAlignItems.Center,
                    BackgroundColor = Colors.White,
                    Children =
                    {
                        new Image
                        {
                            Source = "download.gif",
                            HeightRequest = 100,
                            WidthRequest = 100,
                            Margin = new(0, 28, 0, 0)
                        },
                        new Label
                        {
                            Text = title,
                            FontSize = 32,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                        },
                        new Button
                        {
                            Text = "Stop",
                            CornerRadius = 8,
                            Margin = new(4),
                            Command = new Command(() => Close()),
                        }.AlignSelf(FlexAlignSelf.Center)
                    }
                }
            };

        }
    }
}