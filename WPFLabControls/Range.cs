using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;

using LabControls.Common;

namespace LabControls
{
    public class Range : FrameworkElement, INotifyPropertyChanged
    {


        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(double), typeof(Range), new PropertyMetadata(-1.0, StartChangedCallback));
        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        static void StartChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Range;
            model.OnPropertyChanged("Start");
            model.OnRangeUpdate();
        }

        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(double), typeof(Range), new PropertyMetadata(1.0, EndChangedCallback ));
        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }
        static void EndChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Range;
            model.OnPropertyChanged("End");
            model.OnRangeUpdate();
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(Range), new PropertyMetadata(new SolidColorBrush(Colors.Black), FillChangedCallback));
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        static void FillChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Range;
            model.OnPropertyChanged("Fill");
            model.OnRangeUpdate();
        }

        public event EventHandler RangeUpdate;

        void OnRangeUpdate()
        {
            if (RangeUpdate !=null)
                RangeUpdate(this, new EventArgs());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        

        void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
