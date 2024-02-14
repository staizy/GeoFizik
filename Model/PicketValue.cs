using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFizik.Model
{
    public class PicketValue : PropChange
    {
        int id;
        Picket picket;
        double value;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public Picket Picket
        {
            get { return Picket; }
            set
            {
                Picket = value;
                OnPropertyChanged(nameof(Picket));
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
