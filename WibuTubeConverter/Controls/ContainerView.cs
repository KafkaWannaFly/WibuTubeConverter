using CommunityToolkit.Maui.Markup;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace WibuTubeConverter.Controls
{
    public partial class ContainerView : ScrollView
    {
        public View Child { get; set; }

        public ContainerView()
        {
            InitContainer();
        }

        public ContainerView(View view)
        {
            Child = view;
            InitContainer();
        }

        private void InitContainer()
        {
            Content = new Grid()
            {
                ColumnDefinitions = Columns.Define(Star, Stars(8), Star),
                Children =
                {
                    Child.Column(1)
                }
            };
        }
    }
}
