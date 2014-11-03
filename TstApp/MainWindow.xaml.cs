using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WPFLabControls;

namespace TstApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            
            /*var model = new ScaleModel();

            model.Radius = 150;

            model.MinScaleRange = -180;
            model.MaxScaleRange = 90;

            model.MinValue = -10;
            model.MaxValue = 10;

            model.MajorTicksCount = 20;
            model.MinorTicksCount = 5;


            foreach (var tick in model.MajorTicks)
            {
                var rect = new Rectangle();
                rect.Width = 2;
                rect.Height = 10;
                rect.Fill = new SolidColorBrush(Colors.Black);

                var tickAngle = model.Convert(tick);

                AddTick(model.Radius, tickAngle, rect);
                
            }

            foreach (var tick in model.MinorTicks)
            {
                var rect = new Rectangle();
                rect.Width = 2;
                rect.Height = 4;
                rect.Fill = new SolidColorBrush(Colors.Black);

                var tickAngle = model.Convert(tick);

                AddTick(model.Radius, tickAngle, rect);
            }

            foreach (var tick in model.MajorTicks)
            {
                var rect = new Rectangle();
                rect.Width = 2;
                rect.Height = 10;
                rect.Fill = new SolidColorBrush(Colors.Black);

                var tickAngle = model.Convert(tick);

                AddTick2( tickAngle, rect);

            }

            foreach (var tick in model.MinorTicks)
            {
                var rect = new Rectangle();
                rect.Width = 2;
                rect.Height = 4;
                rect.Fill = new SolidColorBrush(Colors.Black);

                var tickAngle = model.Convert(tick);

                AddTick2(tickAngle, rect);
            }






            

            Point StartPoint = new Point(R * Math.Cos(DegToRad(model.MinScaleRange - 90)), R * Math.Sin(DegToRad(model.MinScaleRange - 90)));

            Point EndPoint = new Point(R * Math.Cos(DegToRad(EndAngle - 90)), R * Math.Sin(DegToRad(EndAngle - 90)));


            Path arc = new Path();
            arc.Stroke = new SolidColorBrush(Colors.Black);
            arc.StrokeThickness = 3;

            var seg = new ArcSegment();
            seg.Point = EndPoint;
            seg.IsLargeArc = (EndAngle - StartAngle) >=180;
            seg.Size = new Size(R, R);

            seg.SweepDirection = SweepDirection.Clockwise;


            var fig = new PathFigure(new Point(0, 0), new ArcSegment[] { seg }, false);
            fig.StartPoint = StartPoint;


            arc.Data = new PathGeometry(new PathFigure[] { fig });

           
            ScaleCanvas.Children.Add(arc);
             * */
        }







        double RadToDeg(double Angle)
        {
            return (Angle * 180) / Math.PI;
        }
        double DegToRad(double Angle)
        {
            return (Angle * Math.PI) / 180;
        }


    }
    /*
    public class RadialScale
    {
        /// <summary>
        /// Начальное значение шкалы
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// Конечное значение шкалы
        /// </summary>
        public double MaxValue { get; set; }
        /// <summary>
        /// Начальный угол
        /// </summary>
        public double MinRange { get; set; }
        /// <summary>
        /// Конечный угол
        /// </summary>
        public double MaxRange { get; set; }
        /// <summary>
        /// Радиус шкалы
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Количество больших отметок на шкале
        /// </summary>
        public int MajorTicksCount { get; set; }
        /// <summary>
        /// Количество малых отметок между большими отметками
        /// </summary>
        public int MinorTicksCount { get; set; }

        public double[] MajorTicks
        {
            get
            {
                var range = MaxValue - MinValue;
                var step = range / (MajorTicksCount + 1);

                var ticks = new double[MajorTicksCount + 2];

                for (int i = 0; i < ticks.Length; i++)
                    ticks[i] = MinValue + i * step;

                return ticks;
            }
        }

        public double[] MinorTicks
        {
            get
            {
                var majRange = MaxValue - MinValue;
                var majStep = majRange / (MajorTicksCount + 1);
                var minStep



                var ticks = new double[MajorTicksCount + 1];

                for (int i = 0; i < ticks.Length; i++)
                    ticks[i] = MinValue + i * step;

                return ticks;
            }
        }

        public double Convert(double Value)
        {
            if (Value <= MinValue)
                return MinRange;

            if (Value >= MaxValue)
                return MaxRange;

            var dAngle = MaxRange - MinRange;
            var dValue = MaxValue - MinValue;

            return (Value - MinValue) * (dAngle / dValue);
        }

        double RadToDeg(double Angle)
        {
            return (Angle * 180) / Math.PI;
        }
        double DegToRad(double Angle)
        {
            return (Angle * Math.PI) / 180;
        }
    }*/
}
