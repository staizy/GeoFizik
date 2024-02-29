using GeoFizik.Model;
using System.Windows;

namespace GeoFizik.View
{
    public partial class AddCustomerDialogWindow : Window
    {
        public AddCustomerDialogWindow(Customer customer)
        {
            InitializeComponent();
            DataContext = customer;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
