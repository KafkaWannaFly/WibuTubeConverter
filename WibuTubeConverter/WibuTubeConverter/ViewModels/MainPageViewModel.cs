using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WibuTubeConverter.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string url;

        [RelayCommand]
        async Task SearchVideo()
        {
            return;
        }
    }
}
