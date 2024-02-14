using GeoFizik.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeoFizik
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using(ApplicationContext db = new ApplicationContext())
{
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Customer customer1 = new Customer { Name = "hi" };

                db.Customers.AddRange(customer1);
                db.SaveChanges();
            }
        }
    }
}