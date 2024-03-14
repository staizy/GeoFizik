using GeoFizik.ViewModel;
using System.Windows;

namespace GeoFizik
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}