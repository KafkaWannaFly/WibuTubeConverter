﻿using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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