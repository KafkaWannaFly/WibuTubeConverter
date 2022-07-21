using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WibuTubeConverter.ViewModels
{
    public partial class ConvertPageViewModel : ObservableObject
    {
        readonly WibuTube wibuTube;

        public ConvertPageViewModel(WibuTube wibuTube)
        {
            this.wibuTube = wibuTube;
        }
    }
}
