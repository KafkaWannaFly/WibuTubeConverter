using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace WibuTubeConverter.Controls;

public partial class ContainerView : ScrollView
{
    public ContainerView()
    {
        Content = new Grid()
        {
            BindingContext = this,
            ColumnDefinitions = Columns.Define(Star, Stars(8), Star),
            Children =
            {
                new ContentPresenter().Column(1)
            }
        };
    }

    public ContainerView(View view)
    {
        Content = new Grid()
        {
            ColumnDefinitions = Columns.Define(Star, Stars(8), Star),
            Children =
            {
                view.Column(1)
            }
        };
    }
}