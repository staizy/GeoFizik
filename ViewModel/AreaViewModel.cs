using GeoFizik.Model;
using GeoFizik.View;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media;

namespace GeoFizik.ViewModel
{
    public class AreaViewModel : PropChange
    {
        ApplicationContext db = ApplicationContext.getInstance();

        Profile selectedProfile;
        AreaPoint selectedPoint;
        public Area Area { get; set; }
        public AreaViewModel() : this(new Area()) { }
        public AreaViewModel(Area area) 
        {
            Area = area;
            AddPointCommand = new(AddPoint);
            DeletePointCommand = new(DeletePoint, (obj) => SelectedPoint != null);
            AddProfileCommand = new(AddProfile);
            DeleteProfileCommand = new(DeleteProfile, (obj) => SelectedProfile != null);
            OpenProfileCommand = new(OpenProfile);
            SavePointCommand = new(SavePoint);
        }

        public RelayCommand AddPointCommand { get; set; }
        public RelayCommand DeletePointCommand { get; set; }
        public RelayCommand AddProfileCommand { get; set; }
        public RelayCommand DeleteProfileCommand { get; set; }
        public RelayCommand OpenProfileCommand { get; set; }
        public RelayCommand SavePointCommand { get; set; }

        public string? AreaName
        {
            get => Area.Name;
            set
            {
                Area.Name = value;
                db.Entry(Area).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        void AddPoint(object obj)
        {
            var point = new AreaPoint() { X = 0, Y = 0, Area = Area };
            db.AreaPoints.Add(point);
            db.SaveChanges();
            SelectedPoint = point;
            OnPropertyChanged(nameof(Area));
        }
        void DeletePoint(object obj)
        {
            db.AreaPoints.Remove(SelectedPoint);
            db.SaveChanges();
        }
        void AddProfile(object obj)
        {
            var point = new Profile() { Area = Area };
            db.Profiles.Add(point);
            db.SaveChanges();
            SelectedProfile = point;
            OnPropertyChanged(nameof(Area));
        }
        void DeleteProfile(object obj)
        {
            db.Profiles.Remove(SelectedProfile);
            db.SaveChanges();
            OnPropertyChanged(nameof(Area));
        }
        void OpenProfile(object obj)
        {
            new ProfileWindow()
            {
                DataContext = new ProfileViewModel((Profile)obj)
            }.ShowDialog();
            OnPropertyChanged(nameof(obj));
        }
        void SavePoint(object obj)
        {
            if (obj is AreaPoint)
            {
                db.Entry((AreaPoint)obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public Profile SelectedProfile
        {
            get => selectedProfile;
            set
            {
                selectedProfile = value;
                OnPropertyChanged(nameof(SelectedProfile));
            }
        }
        public AreaPoint SelectedPoint
        {
            get => selectedPoint;
            set
            {
                selectedPoint = value;
                OnPropertyChanged(nameof(SelectedPoint));
            }
        }
    }
}
