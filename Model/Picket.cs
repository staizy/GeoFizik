﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFizik.Model
{
    public class Picket : PropChange
    {
        int id;
        double x, y;
        Profile profile;
        ObservableCollection<PicketValue> picketValues;
        ObservableCollection<Operator> picketOperator;

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

        public ObservableCollection<PicketValue> PicketValues
        {
            get { return picketValues; }
            set
            {
                picketValues = value;
                OnPropertyChanged(nameof(PicketValues));
            }
        }

        public ObservableCollection<Operator> PicketOperator
        {
            get { return picketOperator; }
            set
            {
                picketOperator = value;
                OnPropertyChanged(nameof(PicketOperator));
            }
        }
    }
}
