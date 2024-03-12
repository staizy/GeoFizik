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



        public void Draw(DrawModel dm, Brush br)
        {
            if (points is null) return;
            dm.DrawPoly(points.Select(p => p.AsPoint).ToArray(), br, 0.2, true);
            foreach (var p in points)
            {
                dm.DrawText($"{p.X};{p.Y}", p.X, p.Y, br, 1);
                dm.DrawCircle(p.X, p.Y, 0.3, br);
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

        public bool IsCorrect()
        {
            for (int i = 0; i < points?.Count; i++)
                for (int j = i + 1; j < points.Count; j++)
                    if (AreCrossing(points[i], points[(i + 1)%points.Count], points[j], points[(j + 1) % points.Count], colideSegments : Math.Abs(i - j) > 1 && !(i == 0 && j == points.Count-1)))
                        return false;
            return true;
        }

        public bool AreCrossing(AreaPoint a1, AreaPoint a2, AreaPoint b1, AreaPoint b2, bool colideSegments = true)
        {
            double CrossProduct(AreaPoint p1, AreaPoint p2, AreaPoint p3)
            {
                return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
            }

            bool IsPointOnSegment(AreaPoint p, AreaPoint q, AreaPoint r)
            {
                if (!colideSegments && (p == q || p == r || q == r)) return false;
                return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                       q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y);
            }

            double cp1 = CrossProduct(a1, a2, b1);
            double cp2 = CrossProduct(a1, a2, b2);
            double cp3 = CrossProduct(b1, b2, a1);
            double cp4 = CrossProduct(b1, b2, a2);

            if ((cp1 > 0 && cp2 < 0 || cp1 < 0 && cp2 > 0) && (cp3 > 0 && cp4 < 0 || cp3 < 0 && cp4 > 0))
                return true;

            if (cp1 == 0 && IsPointOnSegment(a1, b1, a2))
                return true;
            if (cp2 == 0 && IsPointOnSegment(a1, b2, a2))
                return true;
            if (cp3 == 0 && IsPointOnSegment(b1, a1, b2))
                return true;
            if (cp4 == 0 && IsPointOnSegment(b1, a2, b2))
                return true;
            return false;

        }
    }
}
