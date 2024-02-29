using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFizik.Model
{
    public class Operator : PropChange
    {
        private int id;
        private string name;
        private string surname;
        private string? device;
        ObservableCollection<Picket>? pickets;

        public override string ToString()
        {
            return $"{name} {surname}";
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string? Device
        {
            get { return device; }
            set
            {
                device = value;
                OnPropertyChanged(nameof(Device));
            }
        }

        public ObservableCollection<Picket>? Picket
        {
            get { return pickets; }
            set
            {
                pickets = value;
                OnPropertyChanged(nameof(Picket));
            }
        }
    }
}
