using GeoFizik.Model;
using GeoFizik.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GeoFizik.ViewModel
{
    class PicketViewModel : PropChange
    {
        ApplicationContext db = ApplicationContext.getInstance();

        PicketValue selectedPicketValue;
        Operator selectedOperator;

        public Profile Profile { get; set; }
        public Picket Picket { get; set; }
        public Picket PicketValue { get; set; }
        public ObservableCollection<Operator> Operators { get => db.Operators.Local.ToObservableCollection(); }
        

        public PicketViewModel() : this(new Picket()) { }
        public PicketViewModel(Picket picket)
        {
            Picket = picket;
            if (Picket.Operator is not null) SelectedOperator = Picket.Operator;
            AddPicketValueCommand = new(AddPicketValue);
            DeletePicketValueCommand = new(DeletePicketValue, (obj) => SelectedPicketValue != null);
            SavePicketValueCommand = new(SavePicketValue);
            AddOperatorCommand = new(AddOperator);
            DeleteOperatorCommand = new(DeleteOperator, (obj) => SelectedOperator != null);
            RefreshPlotCommand = new(RefreshPlot);
            SetPlotModel();
        }
        public RelayCommand AddPicketValueCommand { get; set; }
        public RelayCommand RefreshPlotCommand { get; set; }
        public RelayCommand DeletePicketValueCommand { get; set; }
        public RelayCommand SavePicketValueCommand { get; set; }
        public RelayCommand AddOperatorCommand { get; set; }
        public RelayCommand DeleteOperatorCommand { get; set; }

        private void SetPlotModel()
        {
            var plotModel = new PlotModel() { Title = "График значений" };

            var xAxis = new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Top, Title = "Амплитуда", StartPosition = 0, EndPosition = 1, IsZoomEnabled = false };
            var yAxis = new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Title = "H Эффективное", StartPosition = 1, EndPosition = 0, IsZoomEnabled = false };

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            for (int i = 0; i < 800; i++)
            {
                var horizontalLineAnnotation = new OxyPlot.Annotations.LineAnnotation
                {
                    Type = OxyPlot.Annotations.LineAnnotationType.Horizontal,
                    Y = i*1,
                    Color = OxyColors.Gray,
                    StrokeThickness = 1,
                    LineStyle = LineStyle.Solid
                };
                plotModel.Annotations.Add(horizontalLineAnnotation);

                var verticalLineAnnotation = new OxyPlot.Annotations.LineAnnotation
                {
                    Type = OxyPlot.Annotations.LineAnnotationType.Vertical,
                    X = i*1,
                    Color = OxyColors.Gray,
                    StrokeThickness = 1,
                    LineStyle = LineStyle.Solid
                };
                plotModel.Annotations.Add(verticalLineAnnotation);
            }


            if (Picket.PicketValues != null && Picket.PicketValues.Any())
            {
                var lineSeries = new LineSeries
                {
                    Title = "График",
                    Color = OxyColors.Black,
                    StrokeThickness = 3,
                    MarkerType = MarkerType.Diamond,
                    MarkerSize = 4, 
                    MarkerStroke = OxyColors.Red,
                    MarkerFill = OxyColors.Red
                };

                foreach (var val in Picket.PicketValues)
                {
                    lineSeries.Points.Add(new DataPoint(val.Amplitude, val.H_value));
                }
                plotModel.Series.Add(lineSeries);
            }

            PlotModel = plotModel;
        }


        void AddOperator(object obj)
        {
            var oper = new Operator() { Name = "", Surname = "" };
            if (new AddOperatorDialogWindow(oper).ShowDialog() == false) return;
            else if (oper.Name == "" || oper.Surname == "")
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение");
            }
            else
            {
                db.Operators.Add(oper);
                db.SaveChanges();
                SelectedOperator = oper;
            }
        }

        void RefreshPlot(object obj)
        {
            SetPlotModel();
        }

        void DeleteOperator(object obj)
        {
            if (MessageBox.Show("Удалить этого оператора?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            else
            {
                db.Operators.Remove(SelectedOperator!);
                SelectedOperator = null!;
                db.SaveChanges();
            }
        }

        void AddPicketValue(object obj)
        {
            var val = new PicketValue() { H_value = 0, Amplitude = 0, Picket = Picket };
            db.PicketValues.Add(val);
            db.SaveChanges();
            selectedPicketValue = val;
            OnPropertyChanged(nameof(Picket));
            SetPlotModel();
        }
        void DeletePicketValue(object obj)
        {
            db.PicketValues.Remove(SelectedPicketValue);
            db.SaveChanges();
            OnPropertyChanged(nameof(Picket));
            SetPlotModel();
        }
        void SavePicketValue(object obj)
        {
            if (obj is PicketValue)
            {
                db.Entry((PicketValue)obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public PicketValue SelectedPicketValue
        {
            get => selectedPicketValue;
            set
            {
                selectedPicketValue = value;
                OnPropertyChanged(nameof(SelectedPicketValue));
                SetPlotModel();
            }
        }

        public Operator? SelectedOperator
        {
            get => selectedOperator;
            set
            {
                selectedOperator = value;
                Picket.Operator = value;
                OnPropertyChanged(nameof(SelectedOperator));
                db.Entry(Picket).State = EntityState.Modified;
                db.SaveChanges();
                SetPlotModel();
            }
        }


        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get => plotModel;
            set
            {
                plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }
    }
}
