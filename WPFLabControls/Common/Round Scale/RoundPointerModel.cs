using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Shapes;


namespace LabControls.Common
{
    public class RoundPointerModel : FrameworkElement, INotifyPropertyChanged
    {
        public Scale ScaleModel
        {
            get
            {
                return scaleModel;
            }
            set
            {
                scaleModel = value;
                UpdateValue();
            }
        }
        Scale scaleModel = null;

        #region Dependency Properties
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(RoundPointerModel), new PropertyMetadata(0.0, ValueChangedCallback));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as RoundPointerModel;
            p.UpdateValue();
        }

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type", typeof(ShapeType), typeof(RoundPointerModel), new PropertyMetadata(ShapeType.Rectangle, TypeChangedCallback));
        public ShapeType Type
                {
                    get { return (ShapeType)GetValue(TypeProperty); }
                    set { SetValue(TypeProperty, value); }
                }
        static void TypeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as RoundPointerModel;
            p.Geometry = Utilities.CreateGeometry(p.Type, p.Size);
        }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(RoundPointerModel), new PropertyMetadata(10.0, RadiusChangedCallback));
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        static void RadiusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as RoundPointerModel;
            p.UpdateTransform();
        }

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            "Size", typeof(Size), typeof(RoundPointerModel), new PropertyMetadata(new Size(2,5), SizeChangedCallback));
        public Size Size
                {
                    get { return (Size)GetValue(SizeProperty); }
                    set { SetValue(SizeProperty, value); }
                }
        static void SizeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as RoundPointerModel;
            p.UpdateTransform();
            p.Geometry = Utilities.CreateGeometry(p.Type, p.Size);

        }
        
        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(RoundPointerModel), new PropertyMetadata(0.5, OffsetChangedCallback));
        public double Offset
                {
                    get { return (double)GetValue(OffsetProperty); }
                    set { SetValue(OffsetProperty, value); }
                }
        static void OffsetChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as RoundPointerModel;
            p.UpdateTransform();
        }
        
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof(Brush), typeof(RoundPointerModel), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        public Brush Fill
                {
                    get { return (Brush)GetValue(FillProperty); }
                    set { SetValue(FillProperty, value); }
                }
       
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof(Brush), typeof(RoundPointerModel), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        public Brush Stroke
                {
                    get { return (Brush)GetValue(StrokeProperty); }
                    set { SetValue(StrokeProperty, value); }
                }

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
            "Thickness", typeof(double), typeof(RoundPointerModel), new PropertyMetadata(0.5));
        public double Thickness
                {
                    get { return (double)GetValue(ThicknessProperty); }
                    set { SetValue(ThicknessProperty, value); }
                }

        #endregion
        
        #region Properties

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

        public double XShift
        {
            get
            {
                return xShift;
            }
            private set
            {
                xShift = value;
                RaisePropertyChanged("XShift");
            }
        }
        double xShift;

        public double YShift
        {
            get
            {
                return yShift;
            }
            private set
            {
                yShift = value;
                RaisePropertyChanged("YShift");
            }
        }
        double yShift;

        public double Angle
        {
            get
            {
                return angle;
            }
            private set
            {
                angle = value;
                RaisePropertyChanged("Angle");
            }
        }
        double angle;

        #endregion

        #region Methods

        public FrameworkElement CreatePointer()
        {
            var path = new Path();

            var translate = new TranslateTransform();
            var rotate = new RotateTransform();
            var group = new TransformGroup();

            group.Children.Add(translate);
            group.Children.Add(rotate);

            //path.RenderTransform = group;
           

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

            var XBinding = new Binding("XShift");
            XBinding.Source = this;
            BindingOperations.SetBinding(translate, TranslateTransform.XProperty, XBinding);

            var YBinding = new Binding("YShift");
            YBinding.Source = this;
            BindingOperations.SetBinding(translate, TranslateTransform.YProperty, YBinding);

            var AngleBinding = new Binding("Angle");
            AngleBinding.Source = this;
            BindingOperations.SetBinding(rotate, RotateTransform.AngleProperty, AngleBinding);

            var canvas = new Canvas();
            canvas.Children.Add(path);
            canvas.RenderTransform = group;
            return canvas;
        }

        void UpdateTransform()
        {
            XShift = -Size.Width / 2;
            YShift = -Radius + Offset;

            if (ScaleModel != null)
                Angle = ScaleModel.ScaleForward(Value);
            else
                Angle = 0;
        }

        void UpdateValue()
       {
           if (ScaleModel != null)
               Angle = ScaleModel.ScaleForward(Value);
           else
               Angle = 0;
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
