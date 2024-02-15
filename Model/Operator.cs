using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFizik.Model
{
    public class Operator : PropChange
    {
        private int id;
        private string name;
        private string suranme;
        private string device;
        Picket picket;

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
            get { return suranme; }
            set
            {
                suranme = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string Device
        {
            get { return device; }
            set
            {
                device = value;
                OnPropertyChanged(nameof(Device));
            }
        }

        public Picket Picket
        {
            get { return picket; }
            set
            {
                picket = value;
                OnPropertyChanged(nameof(Picket));
            }
        }
    }
}
