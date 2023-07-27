namespace WibuTubeConverter.Controls
{
    [AllBindableProps]
    public partial class TextInput : ContentView
    {
        private string _text;
        private string _placeHolder;

        public TextInput()
        {
            Content = new Border()
            {
                BindingContext = this,
                Content = new Entry()
                    {
                        ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                        VerticalTextAlignment = TextAlignment.Center,
                    }
                    .Bind(Entry.TextProperty, nameof(Text))
                    .Bind(Entry.PlaceholderProperty, nameof(PlaceHolder))
            }.Bind(StyleProperty, nameof(Style));
        }
    }
}