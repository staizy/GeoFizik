using GeoFizik.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using GeoFizik.View;
using System.Drawing;

namespace GeoFizik.ViewModel
{
    public class ProfileViewModel : PropChange
    {
        ApplicationContext db = ApplicationContext.getInstance();
        DrawingImage image;
        Random rnd = new Random();

        Picket selectedPicket;
        ProfilePoint selectedPoint;

        public Profile Profile { get; set; }
        public Picket Picket { get; set; }

        public ProfileViewModel() : this(new Profile()) { }
        public ProfileViewModel(Profile profile)
        {
            Profile = profile;
            AddPointCommand = new(AddPoint);
            DeletePointCommand = new(DeletePoint, (obj) => SelectedPoint != null);
            AddPicketCommand = new(AddPicket);
            DeletePicketCommand = new(DeletePicket, (obj) => SelectedPicket != null);
            SavePicketCommand = new(SavePicket);
            SavePointCommand = new(SavePoint);
            OpenPicketCommand = new(OpenPicket);
            AddRandomPointCommand = new(AddRandomPoint);
            Redraw();
        }
        public RelayCommand AddPointCommand { get; set; }
        public RelayCommand DeletePointCommand { get; set; }
        public RelayCommand AddPicketCommand { get; set; }
        public RelayCommand OpenPicketCommand { get; set; }
        public RelayCommand DeletePicketCommand { get; set; }
        public RelayCommand SavePicketCommand { get; set; }
        public RelayCommand SavePointCommand { get; set; }
        public RelayCommand AddRandomPointCommand { get; set; }

        void AddRandomPoint(object obj)
        {
            if (Profile?.Points == null)
            {
                while (true)
                {
                    int deltaX, deltaY, r = rnd.Next(0, 100);
                    do
                    {
                        deltaX = rnd.Next(-r, r);
                        deltaY = rnd.Next(-r, r);
                    } while (deltaX == 0 && deltaY == 0);
                    Profile.Points = new() { new() { X = deltaX, Y = deltaY, Profile = Profile } };
                    if (Profile.IsCorrect()) break;
                }
            }
            
            while (true)
            {
                var lastPoint = Profile.Points.OrderBy(p => p.Id).Last();
                int deltaX, deltaY, r = rnd.Next(1, 10);
                do
                {
                    deltaX = rnd.Next(-r, r);
                    deltaY = rnd.Next(-r, r);
                } while (deltaX == 0 && deltaY == 0);
                var newpoint = new ProfilePoint() { X = Profile.Points.Last().X + deltaX, Y = Profile.Points.Last().Y + deltaY, Profile = Profile };
                Profile.Points.Add(newpoint);
                if (Profile.IsCorrect())
                {
                    db.ProfilePoints.Add(newpoint);
                    db.SaveChanges();
                    SelectedPoint = newpoint;
                    OnPropertyChanged(nameof(Profile));
                    break;
                }
                else Profile.Points.Remove(newpoint);
            }
        }

        void OpenPicket(object obj)
        {
            new PicketWindow()
            {
                DataContext = new PicketViewModel((Picket)obj)
            }.ShowDialog();
            OnPropertyChanged(nameof(obj));
        }

        void AddPoint(object obj)
        {
            var point = new ProfilePoint() { X = 0, Y = 0, Profile = Profile };
            db.ProfilePoints.Add(point);
            db.SaveChanges();
            SelectedPoint = point;
            OnPropertyChanged(nameof(Profile));
        }
        void DeletePoint(object obj)
        {
            db.ProfilePoints.Remove(SelectedPoint);
            db.SaveChanges();
        }
        void AddPicket(object obj)
        {
            var picket = new Picket() { Profile = Profile };
            db.Pickets.Add(picket);
            db.SaveChanges();
            SelectedPicket = picket;
            OnPropertyChanged(nameof(Profile));
        }
        void DeletePicket(object obj)
        {
            db.Pickets.Remove(SelectedPicket);
            db.SaveChanges();
            OnPropertyChanged(nameof(Profile));
        }
        void SavePicket(object obj)
        {
            if (obj is Picket)
            {
                db.Entry((Picket)obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        void SavePoint(object obj)
        {
            if (obj is ProfilePoint)
            {
                db.Entry((ProfilePoint)obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public Picket SelectedPicket
        {
            get => selectedPicket;
            set
            {
                selectedPicket = value;
                OnPropertyChanged(nameof(SelectedPicket));
                Redraw();
            }
        }
        public ProfilePoint SelectedPoint
        {
            get => selectedPoint;
            set
            {
                selectedPoint = value;
                OnPropertyChanged(nameof(SelectedPoint));
                Redraw();
            }
        }

        public DrawingImage Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        void Redraw()
        {
            var newimage = new DrawModel();
            if (Profile.IsCorrect()) Profile.Draw(newimage, Brushes.Gray);
            else Profile.Draw(newimage, Brushes.Red);
            foreach (var p in Profile?.Points ?? new()) 
            {
                if (p == SelectedPoint) newimage.DrawCircle(p.X, p.Y, 0.15, Brushes.Green);
                else newimage.DrawCircle(p.X, p.Y, 0.15, Brushes.DarkGray);
            }
            foreach (var p in Profile?.Pickets ?? new())
            {
                if (p == SelectedPicket && p.IsOnProfile()) newimage.DrawFlag(p.X, p.Y, 0.17, Brushes.Red);
                else if (p == SelectedPicket && !p.IsOnProfile()) newimage.DrawFlag(p.X, p.Y, 0.17, Brushes.Black);
                else if (p != SelectedPicket && p.IsOnProfile()) newimage.DrawFlag(p.X, p.Y, 0.17, Brushes.DarkRed);
                else if (p != SelectedPicket && !p.IsOnProfile()) newimage.DrawFlag(p.X, p.Y, 0.17, Brushes.DarkOliveGreen);
            }
            Image = newimage.Render();
        }
    }
}
