using GeoFizik.Model;
using GeoFizik.View;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Input;

namespace GeoFizik.ViewModel
{
    public class DBConnectViewModel : PropChange
    {
        private string dbName;
        public string DbName
        {
            get { return dbName; }
            set
            {
                dbName = value;
                OnPropertyChanged(nameof(DbName));
            }
        }

        public ICommand ConnectCommand { get; set; }
        public ICommand EnterDefaultCommand { get; set; }

        public DBConnectViewModel()
        {
            ConnectCommand = new RelayCommand(ConnectToDatabase);
            EnterDefaultCommand = new RelayCommand(EnterDefault);
        }

        private void EnterDefault(object obj)
        {
            DbName = "GeoKrizo2024";
            OnPropertyChanged(nameof(DbName));
        }

        private void ConnectToDatabase(object obj)
        {
            if (!string.IsNullOrWhiteSpace(DbName))
            {
                using (var db = new ApplicationContext(DbName.ToString()))
                {
                    bool exists = db.Database.CanConnect();
                    if (exists)
                    {
                        MessageBox.Show($"База данных {DbName} существует, подключаемся.", "Подключение");                        
                    }
                    else
                    {
                        db.Database.EnsureCreated();
                        MessageBox.Show($"Создана новая база данных {DbName}, подключаемся.", "Подключение");
                    }
                    var win = new MainWindow()
                    {
                        DataContext = new MainViewModel()
                    };
                    win.Show();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = win;
                    OnPropertyChanged(nameof(obj));
                }
            }
        }
    }
}
