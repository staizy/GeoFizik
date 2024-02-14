using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFizik.Model
{
    public class Profile : PropChange
    {
        int id;
        Area area;
        ObservableCollection<Picket> pickets;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public Area Area
        {
            get { return area; }
            set
            {
                area = value;
                OnPropertyChanged(nameof(Area));
            }
        }
        public ObservableCollection<Picket> Pickets
        {
            get { return pickets; }
            set
            {
                pickets = value;
                OnPropertyChanged(nameof(Pickets));
            }
        }
    }
}
