using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Shapes;
using LabControls.Common;


namespace LabControls
{
    public class Pointer : FrameworkElement, INotifyPropertyChanged
    {
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type", typeof(ShapeType), typeof(Pointer), new PropertyMetadata(ShapeType.Rectangle, TypeChangedCallback));
        public ShapeType Type
        {
            get { return (ShapeType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        static void TypeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as Pointer;
            p.Geometry = Utilities.CreateGeometry(p.Type, p.Size);
        }

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            "Size", typeof(Size), typeof(Pointer), new PropertyMetadata(new Size(2, 5), SizeChangedCallback));
        public Size Size
        {
            get { return (Size)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }
        static void SizeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as Pointer;
            p.Geometry = Utilities.CreateGeometry(p.Type, p.Size);

        }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof(Brush), typeof(Pointer), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        static void FillChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Pointer;
            model.RaisePropertyChanged("Fill");
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof(Brush), typeof(Pointer), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }
        static void StrokeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Pointer;
            model.RaisePropertyChanged("Stroke");

        }

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
            "Thickness", typeof(double), typeof(Pointer), new PropertyMetadata(0.5));
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        static void ThicknessChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Pointer;
            model.RaisePropertyChanged("Thickness");
        }

        public Geometry Geometry
        {
            get
            {
                return geometry;
            }
            private set
            {
                geometry = value;
                RaisePropertyChanged("Geometry");
            }
        }
        Geometry geometry = Utilities.CreateGeometry(ShapeType.Rectangle, new Size(2, 5));


        #region Methods

        public FrameworkElement CreatePointer()
        {
            var path = new Path();

            var StrokeBinding = new Binding("Stroke");
            StrokeBinding.Source = this;
            path.SetBinding(Path.StrokeProperty, StrokeBinding);

            var FillBinding = new Binding("Fill");
            FillBinding.Source = this;
            path.SetBinding(Path.FillProperty, FillBinding);

            var ThicknessBinding = new Binding("Thickness");
            ThicknessBinding.Source = this;
            path.SetBinding(Path.StrokeThicknessProperty, ThicknessBinding);

            var GeometryBinding = new Binding("Geometry");
            GeometryBinding.Source = this;
            path.SetBinding(Path.DataProperty, GeometryBinding);      

            var canvas = new Canvas();
            canvas.Children.Add(path);
            return canvas;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion
    }
}
