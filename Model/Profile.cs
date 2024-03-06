using System.Collections.ObjectModel;
using System.Windows.Media;

namespace GeoFizik.Model
{
    public class Profile : PropChange
    {
        int id;
        Area area;
        ObservableCollection<Picket> pickets;
        ObservableCollection<ProfilePoint> points;

        public void Draw(DrawModel dm, Brush br)
        {
            if (points is null) return;
            dm.DrawPoly(points.Select(p => p.AsPoint).ToArray(), br, 0.2, false);
            foreach (var p in points)
            {
                dm.DrawText($"{p.X};{p.Y}", p.X, p.Y, br, 0.7);
                dm.DrawCircle(p.X, p.Y, 0.2, br);
            }
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

        public ObservableCollection<ProfilePoint> Points
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
