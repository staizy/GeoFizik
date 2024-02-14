using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFizik.Model
{
    public class Picket : PropChange
    {
        int id;
        Profile profile;
        double x, y, value;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public Profile Profile
        {
            get { return profile; }
            set
            {
                profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        public double Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
}
