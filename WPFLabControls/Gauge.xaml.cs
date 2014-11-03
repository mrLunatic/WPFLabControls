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
using System.Collections.ObjectModel;
using LabControls.Common;

namespace LabControls
{
    /// <summary>
    /// Логика взаимодействия для Gauge.xaml
    /// </summary>
    public partial class Gauge : UserControl
    {
        List<FrameworkElement> RoundRanges = new List<FrameworkElement>();

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(Gauge), new PropertyMetadata(-135.0));
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register("EndAngle", typeof(double), typeof(Gauge), new PropertyMetadata(135.0));
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }

        public static readonly DependencyProperty RangesProperty = DependencyProperty.Register(
            "Ranges", typeof(ObservableCollection<Range>), typeof(Gauge), new PropertyMetadata(RangesPropertyKeyChangedCallback));
        public ObservableCollection<Range> Ranges
        {
            get { return (ObservableCollection<Range>)GetValue(RangesProperty); }
            set { SetValue(RangesProperty, value); }
        }

        static void RangesPropertyKeyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gauge = d as Gauge;
            var oldRanges = e.OldValue as ObservableCollection<Range>;
            var newRanges = e.NewValue as ObservableCollection<Range>;

            if (oldRanges != null)
                oldRanges.CollectionChanged -= gauge.Ranges_CollectionChanged;

            if (newRanges != null)
                newRanges.CollectionChanged += gauge.Ranges_CollectionChanged;

            gauge.UpdateRanges();
            
        }
        void Ranges_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateRanges();
        }
        
        public static readonly DependencyProperty MinorTickProperty = DependencyProperty.Register(
            "MinorTick", typeof(Tick), typeof(Gauge), new PropertyMetadata());
        public Tick MinorTick
        {
            get { return (Tick)GetValue(MinorTickProperty); }
            set { SetValue(MinorTickProperty, value); }
        }
        
        public static readonly DependencyProperty MajorTickProperty = DependencyProperty.Register(
            "MajorTick", typeof(Tick), typeof(Gauge), new PropertyMetadata());
        public Tick MajorTick
        {
            get { return (Tick)GetValue(MajorTickProperty); }
            set { SetValue(MajorTickProperty, value); }
        }
    
        public static readonly DependencyProperty PointerProperty = DependencyProperty.Register(
            "Pointer", typeof(Pointer), typeof(Gauge), new PropertyMetadata());
        public Pointer Pointer
        {
            get { return (Pointer)GetValue(PointerProperty); }
            set { SetValue(PointerProperty, value); }
        }

        public static readonly DependencyProperty NegativeFillProperty = DependencyProperty.Register(
            "NegativeFill", typeof(Brush), typeof(Gauge), new PropertyMetadata());
        public Brush NegativeFill
        {
            get { return (Brush)GetValue(NegativeFillProperty); }
            set { SetValue(NegativeFillProperty, value); }
        }

        public static readonly DependencyProperty PositiveFillProperty = DependencyProperty.Register(
            "PositiveFill", typeof(Brush), typeof(Gauge), new PropertyMetadata());
        public Brush PositiveFill
        {
            get { return (Brush)GetValue(PositiveFillProperty); }
            set { SetValue(PositiveFillProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof(Scale), typeof(Gauge), new PropertyMetadata(ScaleChangedCallback));
        public Scale Scale
        {
            get { return (Scale)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }
        static void ScaleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gauge = d as Gauge;
            var oldScale = e.OldValue as Scale;
            var newScale = e.NewValue as Scale;

            if (oldScale != null)
            {
                oldScale.ScaledMin = gauge.StartAngle;
                oldScale.ScaledMax = gauge.EndAngle;
            }

            if (newScale != null)
            {
                var minBinding = new Binding("StartAngle");
                minBinding.Source = gauge;
                newScale.SetBinding(Scale.ScaledMinProperty, minBinding);

                var maxBinding = new Binding("EndAngle");
                maxBinding.Source = gauge;
                newScale.SetBinding(Scale.ScaledMaxProperty, maxBinding);

            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(Gauge), new PropertyMetadata(0.0));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty RampProperty = DependencyProperty.Register(
            "Ramp", typeof(Ramp), typeof(Gauge), new PropertyMetadata());
        public Ramp Ramp
        {
            get { return (Ramp)GetValue(RampProperty); }
            set { SetValue(RampProperty, value); }
        }
        
        private void UpdateRanges()
        {
            foreach (var roundRange in RoundRanges)
                gaugeCanvas.Children.Remove(roundRange);

            foreach (var range in Ranges)
            {
                var roundRange = CreateRange(range);
                RoundRanges.Add(roundRange);
                gaugeCanvas.Children.Add(roundRange);
            }
        }

        public Gauge()
        {
            InitializeComponent();
            SetValue(RangesProperty, new ObservableCollection<Range>());

            UpdateRanges();

            DataContext = this;
        }


        FrameworkElement CreateRange(Range Range)
        {
            var roundRange = new RoundRangeView();

            roundRange.Thickness = 10;
            roundRange.Offset = 5; 

            var RadiusBinding = new Binding("Radius");
            RadiusBinding.ElementName = "roundScale";
            roundRange.SetBinding(RoundRangeView.RadiusProperty, RadiusBinding);

            var ScaleBinding = new Binding("Scale");
            roundRange.SetBinding(RoundRangeView.ScaleModelProperty, ScaleBinding);

            var RangeBinding = new Binding();
            RangeBinding.Source = Range;
            roundRange.SetBinding(RoundRangeView.RangeModelProperty, RangeBinding);

            return roundRange;

        }
    }

    public class WidthConverter :IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double Width = (double)values[0];
            double Height = (double)values[1];

            double Offset = double.Parse( (string)parameter);

            return (Math.Min(Width, Height)/2 - Offset);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
