using Microsoft.Maui.Controls.Shapes;

namespace WibuTubeConverter.Controls
{
    [AllBindableProps]
    public partial class TextInput : ContentView
    {
        string text;
        string placeHolder;

        public TextInput()
        {
            Content = new Border()
            {
                BindingContext = this,
                StrokeShape = new RoundRectangle()
                {
                    CornerRadius = 8,
                },
                Content = new Entry()
                {
                    ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                    VerticalTextAlignment = TextAlignment.Center,
                }
                .Bind(Entry.TextProperty, nameof(Text))
                .Bind(Entry.PlaceholderProperty, nameof(PlaceHolder))
            };
        }
    }
}