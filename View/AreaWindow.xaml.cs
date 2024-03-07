using GeoFizik.ViewModel;
using System.Windows;

namespace GeoFizik.View
{
    public partial class AreaWindow : Window
    {
        public AreaWindow()
        {
            InitializeComponent();
            DataContext = new AreaViewModel();
        }
    }
}
