using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GeoFizik.Model
{
    public class Area : PropChange
    {
        int id;
        string? name;
        Project project;
        ObservableCollection<Profile> profiles;
        ObservableCollection<AreaPoint> points;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Project Project
        {
            get { return project; }
            set
            {
                project = value;
                OnPropertyChanged(nameof(Project));
            }
        }

        public ObservableCollection<Profile> Profiles
        {
            get { return profiles; }
            set
            {
                profiles = value;
                OnPropertyChanged(nameof(Profiles));
            }
        }
        public ObservableCollection<AreaPoint> Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged(nameof(Points));
            }
        }
    }
}
