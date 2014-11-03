using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using LabControls.Common;

namespace LabControls
{
    /// <summary>
    /// Общая модель шкалы
    /// </summary>
    public class Scale : FrameworkElement, IValueConverter, INotifyPropertyChanged
    {
        double a, b;

        #region Properties

        internal static readonly DependencyProperty ScaledMinProperty = DependencyProperty.Register(
            "ScaledMin", typeof(double), typeof(Scale), new PropertyMetadata(-50.0, UpdateScaleCallback));
        static void UpdateScaleCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Scale;
            model.UpdateScale();
        }
        internal double ScaledMin
        {
            get { return (double)GetValue(ScaledMinProperty); }
            set { SetValue(ScaledMinProperty, value); }
        }

        internal static readonly DependencyProperty ScaledMaxProperty =
            DependencyProperty.Register("ScaledMax", typeof(double), typeof(Scale), new PropertyMetadata(50.0, UpdateScaleCallback));
        internal double ScaledMax
        {
            get { return (double)GetValue(ScaledMaxProperty); }
            set { SetValue(ScaledMaxProperty, value); }
        }

        /// <summary>
        /// Диапазон отмасштабированных значений
        /// </summary>
        public double ScaledRange
        {
            get
            {
                return scaledRange;
            }
            private set
            {
                scaledRange = value;
                RaisePropertyChanged("ScaleRange");
            }
        }
        double scaledRange = 100;

        // Using a DependencyProperty as the backing store for ValueMin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueMinProperty =
            DependencyProperty.Register("ValueMin", typeof(double), typeof(Scale), new PropertyMetadata(-1.0, UpdateScaleCallback));
        public double ValueMin
        {
            get { return (double)GetValue(ValueMinProperty); }
            set { SetValue(ValueMinProperty, value); }
        }
   

        // Using a DependencyProperty as the backing store for ValueMax.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueMaxProperty =
            DependencyProperty.Register("ValueMax", typeof(double), typeof(Scale), new PropertyMetadata(1.0, UpdateScaleCallback));
        public double ValueMax
        {
            get { return (double)GetValue(ValueMaxProperty); }
            set { SetValue(ValueMaxProperty, value); }
        }

        /// <summary>
        /// Диапазон входных значений
        /// </summary>
        public double ValueRange
        {
            get
            {
                return valueRange;
            }
            private set
            {
                valueRange = value;
                RaisePropertyChanged("ValueRange");
            }
        }
        double valueRange = 2;

        public double MajorStep
        {
            get { return (double)GetValue(MajorStepProperty); }
            set { SetValue(MajorStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MajorStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MajorStepProperty =
            DependencyProperty.Register("MajorStep", typeof(double), typeof(Scale), new PropertyMetadata(10.0, MajorStepCallback));
        static void MajorStepCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Scale;
            model.UpdateMajorTicks();
            model.UpdateLabels();
        }



        public double MinorStep
        {
            get { return (double)GetValue(MinorStepProperty); }
            set { SetValue(MinorStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinorStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinorStepProperty =
            DependencyProperty.Register("MinorStep", typeof(double), typeof(Scale), new PropertyMetadata(2.0, MinorStepCallback));
        static void MinorStepCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as Scale;
            model.UpdateMinorTicks();
        }



        public string FormatString
        {
            get { return (string)GetValue(FormatStringProperty); }
            set { SetValue(FormatStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormatString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatStringProperty =
            DependencyProperty.Register("FormatString", typeof(string), typeof(Scale), new PropertyMetadata("F"));

        


        /// <summary>
        /// Отмасштабированные значения больших делений
        /// </summary>
        public ScaleTick[] MajorTicks
        {
            get
            {
                return majorTicks;
            }
            private set
            {
                majorTicks = value;
                RaisePropertyChanged("MajorTicks");

                if (MajorTicksUpdate != null)
                    MajorTicksUpdate(this, new TicksUpdateArgs(MajorTicks));
            }
        }
        ScaleTick[] majorTicks;

        /// <summary>
        /// Отмасштабированные значений малых делений
        /// </summary>
        public ScaleTick[] MinorTicks
        {
            get
            {
                return minorTicks;
            }
            private set
            {
                minorTicks = value;
                RaisePropertyChanged("MinorTicks");
                if (MinorTickUpdate != null)
                    MinorTickUpdate(this, new TicksUpdateArgs(minorTicks));
            }
        }
        ScaleTick[] minorTicks;

        /// <summary>
        /// Значения подписей на больших делениях
        /// </summary>
        public ScaleLabel[] Labels
        {
            get
            {
                return labels;
            }
            private set
            {
                labels = value;
                RaisePropertyChanged("Labels");
                if (LabelsUpdate != null)
                    LabelsUpdate(this, new LabelsUpdateArgs(labels));
            }
        }
        ScaleLabel[] labels;


        #endregion

        public Scale()
        {
            UpdateScale();
        }

        /// <summary>
        /// Событие изменения списка больших делений
        /// </summary>
        public event EventHandler<TicksUpdateArgs> MajorTicksUpdate;

        /// <summary>
        /// Событие изменения списка малых делений
        /// </summary>
        public event EventHandler<TicksUpdateArgs> MinorTickUpdate;

        /// <summary>
        /// Событие изменения списка отметок
        /// </summary>
        public event EventHandler<LabelsUpdateArgs> LabelsUpdate;

        public event EventHandler ScaleUpdate;

        public double ScaleForward(double Value)
        {
            if (Value > ValueMax)
                return ScaledMax;

            if (Value < ValueMin)
                return ScaledMin;


            return (Value * a) + b;
        }
        public double ScaleBack(double Value)
        {
            if (Value > ScaledMax)
                return ValueMax;

            if (Value < ScaledMin)
                return ValueMin;

            return (Value - b) / a;
        }

        void UpdateScale()
        {
            this.ScaledRange = ScaledMax - ScaledMin;
            this.ValueRange = ValueMax - ValueMin;

            this.a = ScaledRange / ValueRange;
            this.b = ScaledMax - a * ValueMax;

            if (ScaleUpdate != null)
                ScaleUpdate(this, new EventArgs());

            UpdateTicks();
            UpdateLabels();
        }
        void UpdateTicks()
        {
            UpdateMinorTicks();
            UpdateMajorTicks();
        }
        void UpdateMinorTicks()
        {
            var minTicksCount = (int)Math.Ceiling(ValueRange / MinorStep) + 1;

            var minTicks = new List<ScaleTick>();

            for (int i = 0; i < minTicksCount; i++)
            {
                var tickvalue = ValueMin + (i * MinorStep);

               // if (tickvalue%MajorStep != 0)
               //     minTicks.Add(new ScaleTick( tickvalue));

                if ((i * MinorStep) % MajorStep != 0)
                     minTicks.Add(new ScaleTick( tickvalue));
            }


            MinorTicks = minTicks.ToArray(); ;
        }
        void UpdateMajorTicks()
        {
            var majTicksCount = (int)Math.Ceiling(ValueRange / MajorStep) + 1;
            var majTicks = new ScaleTick[majTicksCount];
            for (int i = 0; i < majTicksCount; i++)
            {
                var tickValue = ValueMin + i * MajorStep;
                majTicks[i] = new ScaleTick(tickValue);
            }
            MajorTicks = majTicks;
        }
        void UpdateLabels()
        {
            var labelsCount = (int)Math.Ceiling(ValueRange / MajorStep) + 1;

            var labels = new ScaleLabel[labelsCount];

            for (int i = 0; i < labelsCount; i++)
            {
                var tickValue = ValueMin + i * MajorStep;
               // labels[i] = new ScaleLabel(tickValue, tickValue.ToString(this.StringFormat));
                if (tickValue < ValueMax)
                    labels[i] = new ScaleLabel(tickValue, tickValue.ToString(this.FormatString));
                else
                {
                    labels[i] = new ScaleLabel(ValueMax, ValueMax.ToString(this.FormatString));
                    break;
                }
            }

            Labels = labels;
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

        #region IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Input = (double)value;
            return ScaleForward(Input);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Input = (double)value;
            return ScaleBack(Input);
        }

        #endregion
    }
}
