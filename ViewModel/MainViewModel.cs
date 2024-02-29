using GeoFizik.Model;
using GeoFizik.View;
using GeoMeasure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace GeoFizik.ViewModel
{
    public class MainViewModel : PropChange
    {
        ApplicationContext db = ApplicationContext.getInstance();

        Customer selectedCustomer;
        Project selectedProject;
        Area selectedArea;
        public ObservableCollection<Customer> Customers { get => db.Customers.Local.ToObservableCollection(); }

        public MainViewModel()
        {
            AddCustomerCommand = new(AddCustomer);
            AddProjectCommand = new(AddProject, (obj) => SelectedCustomer != null);
            DeleteProjectCommand = new(DeleteProject, (obj)=> SelectedProject != null);
            DeleteCustomerCommand = new(DeleteCustomer, (obj) => SelectedCustomer != null);
            AddAreaCommand = new(AddArea, (o) => SelectedProject != null);
            DeleteAreaCommand = new(DeleteArea, (o) => SelectedArea != null);
            OpenAreaCommand = new(OpenArea);
        }
        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand AddCustomerCommand { get; set; }
        public RelayCommand AddProjectCommand { get; set; }
        public RelayCommand DeleteProjectCommand { get; set; }
        public RelayCommand AddAreaCommand { get; set; }
        public RelayCommand DeleteAreaCommand { get; set; }
        public RelayCommand OpenAreaCommand { get; set; }

        void DeleteCustomer(object obj)
        {
            if (MessageBox.Show("Удалить этого заказчика?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            else
            {
                db.Customers.Remove(SelectedCustomer);
                db.SaveChanges();
            }
        }

        void AddCustomer(object obj)
        {
            var customer = new Customer() { Name = "", Email = "" };
            if (new AddCustomerDialogWindow(customer).ShowDialog() == false) return;
            else if (customer.Name == "" || customer.Email == "")
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение");
            }
            else
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                SelectedCustomer = customer;
            }            
        }

        void DeleteProject(object obj)
        {
            if (MessageBox.Show("Удалить этот проект?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            else
            {
                db.Projects.Remove(SelectedProject);
                db.SaveChanges();
            }
        }

        void AddProject(object obj)
        {
            var project = new Project() { Name = "", Address = "", Customer = SelectedCustomer };
            if (new AddProjectDialogWindow(project).ShowDialog() == false) return;
            else if (project.Name == "" || project.Address == "")
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение");
            }
            else
            {
                db.Projects.Add(project);
                db.SaveChanges();
                SelectedProject = project;
            }
        }

        void AddArea(object obj)
        {
            var a = new Area() { Project = SelectedProject };
            db.Areas.Add(a);
            db.SaveChanges();
            a.Name = $"Площадь {a.Id}";
            db.SaveChanges();
            OnPropertyChanged(nameof(SelectedProject));
            SelectedArea = a;
        }
        void DeleteArea(object obj)
        {
            if (MessageBox.Show("Удалить выбранную площадь?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            db.Areas.Remove(SelectedArea);
            db.SaveChanges();
            OnPropertyChanged(nameof(SelectedProject));
            db.Areas.Load();
        }
        void OpenArea(object obj)
        {
            new AreaWindow()
            {
                DataContext = new AreaViewModel((Area)obj)
            }.ShowDialog();
            OnPropertyChanged(nameof(SelectedProject.Areas));
            OnPropertyChanged(nameof(obj));
        }

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }
        public Project SelectedProject
        {
            get => selectedProject;
            set
            {
                selectedProject = value;
                OnPropertyChanged();
            }
        }

        public Area SelectedArea
        {
            get => selectedArea;
            set
            {
                selectedArea = value;
                OnPropertyChanged();
            }
        }
    }
}
