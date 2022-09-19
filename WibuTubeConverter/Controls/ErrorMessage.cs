using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace WibuTubeConverter.Controls
{
    [AllBindableProps]
    public partial class ErrorMessage : ContentView
    {
        string text;

        public ErrorMessage()
        {
            Content = new Border
            {
                BindingContext = this,
                Stroke = Colors.Red,
                BackgroundColor = Color.FromRgba("#bb00001A"),
                Padding = new Thickness(8),
                Content = new Grid
                {
                    ColumnDefinitions = Columns.Define(Auto, Star),
                    ColumnSpacing = 8,
                    Children =
                    {
                        new Image()
                        {
                            Source = ImageSource.FromFile("warning.png"),
                            WidthRequest = 20,
                        }.Column(0),
                        new Label
                        {
                            TextColor = Colors.Red
                        }
                            .Column(1)
                            .Bind(Label.TextProperty, nameof(Text)),
                    }
                }
            };
        }
    }
}
