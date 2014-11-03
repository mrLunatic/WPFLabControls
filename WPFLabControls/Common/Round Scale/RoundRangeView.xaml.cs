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
using System.ComponentModel;

namespace LabControls.Common
{
    /// <summary>
    /// Логика взаимодействия для RoundRange.xaml
    /// </summary>
    public partial class RoundRangeView : UserControl, INotifyPropertyChanged
    {
        public RoundRangeView()
        {
            InitializeComponent();


        }

        void RangeModel_RangeUpdate(object sender, EventArgs e)
        {
            UpdateRange();
        }

        void ScaleModel_ScaleUpdate(object sender, EventArgs e)
        {
            UpdateRange();
        }

        public static readonly DependencyProperty RangeModelProperty = DependencyProperty.Register(
            "RangeModel", typeof(Range), typeof(RoundRangeView), new PropertyMetadata(RangeModelChangedCallback));
        public Range RangeModel
        {
            get { return (Range)GetValue(RangeModelProperty); }
            set { SetValue(RangeModelProperty, value); }
        }
        static void RangeModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as RoundRangeView;

            var oldModel = e.OldValue as Range;
            var newModel = e.NewValue as Range;

            if (oldModel != null)
                oldModel.RangeUpdate -= model.RangeModel_RangeUpdate;

            if (newModel != null)
                newModel.RangeUpdate += model.RangeModel_RangeUpdate;

            model.UpdateRange();
        }

        public static readonly DependencyProperty ScaleModelProperty = DependencyProperty.Register(
            "ScaleModel", typeof(Scale), typeof(RoundRangeView), new PropertyMetadata(ScaleModelChangedCallback));
        public Scale ScaleModel
        {
            get { return (Scale)GetValue(ScaleModelProperty); }
            set { SetValue(ScaleModelProperty, value); }
        }
        static void ScaleModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as RoundRangeView;

            var oldScale = e.OldValue as Scale;
            var newScale = e.NewValue as Scale;

            if (oldScale != null)
                oldScale.ScaleUpdate -= model.ScaleModel_ScaleUpdate;

            if (newScale != null)
                newScale.ScaleUpdate += model.ScaleModel_ScaleUpdate;

            model.UpdateRange();
        }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(RoundRangeView), new PropertyMetadata(10.0, RadiusChangedCallback));
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        static void RadiusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as RoundRangeView;
            model.UpdateRange();
        }

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(RoundRangeView), new PropertyMetadata(0.5, OffsetChangedCallback));
        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }
        static void OffsetChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as RoundRangeView;
            model.UpdateRange();
        }

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
           "Thickness", typeof(double), typeof(RoundRangeView), new PropertyMetadata(1.0, ThicknessChangedCallback));
        public double Thickness
        {
            get
            {
                return (double)this.GetValue(ThicknessProperty);
            }
            set
            {
                this.SetValue(ThicknessProperty, value);
            }
        }
        static void ThicknessChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as RoundRangeView;
            model.UpdateRange();
        }


        FrameworkElement CreateRange()
        {
            var startAngle = ScaleModel.ScaleForward(RangeModel.Start);
            var endAngle = ScaleModel.ScaleForward(RangeModel.End);
            var geom = Utilities.CreateArc(startAngle, endAngle, Radius - Offset);

            var arcPath = new Path();
            arcPath.Stroke =RangeModel.Fill;
            arcPath.StrokeThickness = Thickness;
            arcPath.Data = geom;

            return arcPath;
        }

        void UpdateRange()
        {
            RangeCanvas.Children.Clear();

            if ((RangeModel != null) && (ScaleModel != null))
                RangeCanvas.Children.Add(CreateRange());
        }


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
