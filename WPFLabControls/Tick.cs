using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;

using LabControls.Common;

namespace LabControls
{

    public class Tick : FrameworkElement, INotifyPropertyChanged
    {
        #region Fields

        Geometry geometry = Utilities.CreateGeometry(ShapeType.Rectangle, new Size(2, 5));

        #endregion

        #region Properties

        /// <summary>
        /// Размер
        /// </summary>
        public Size Size
        {
            get { return (Size)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        /// <summary>
        /// Заливка заполнения
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Заливка обводки
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// Толщина обводки
        /// </summary>
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        /// <summary>
        /// Форма указателя
        /// </summary>
        public ShapeType Type
        {
            get { return (ShapeType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        ///  XAML-геометрия
        /// </summary>
        internal Geometry Geometry
        {
            get
            {
                return geometry;
            }
            private set
            {
                geometry = value;
                OnPropertyChanged("Geometry");
            }
        }
        
        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(Size), typeof(Tick), new PropertyMetadata(new Size(1, 5), UpdateGeometryCallback));

        static void UpdateGeometryCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Tick;
            model.UpdateGeometry();
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(Tick), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(Tick), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(Tick), new PropertyMetadata(0.5));

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(ShapeType), typeof(Tick), new PropertyMetadata(ShapeType.Rectangle, UpdateGeometryCallback));

        #endregion

        #region Constructors

        public Tick()
        { }

        #endregion

        #region Methods

        public FrameworkElement CreateTick()
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

            return path;

           // return Utilities.CreateShape(Type, Size, Stroke, Thickness, Fill);
        }

        void UpdateGeometry()
        {
            Geometry = Utilities.CreateGeometry(Type, Size);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion
    }
}
