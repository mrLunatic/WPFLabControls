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

namespace LabControls.Common
{
    /// <summary>
    /// Логика взаимодействия для ScaleView.xaml
    /// </summary>
    public abstract partial class ScaleView : UserControl
    {
        public ScaleView() : base()
        {
            InitializeComponent();
        }

          #region Fields

        List<FrameworkElement> xamlMajorTicks = new List<FrameworkElement>();
        List<FrameworkElement> xamlMinorTicks = new List<FrameworkElement>();
        List<FrameworkElement> xamlLabels = new List<FrameworkElement>();
        FrameworkElement xamlRamp;


        #endregion Fields


        void RoundScale_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateScale(true); 
        }

        #region Dependency Properties

        public static readonly DependencyProperty MajorTickModelProperty = DependencyProperty.Register(
            "MajorTickModel", typeof(Tick), typeof(ScaleView), new PropertyMetadata(MajorTickModelChangedCallback));
        public Tick MajorTickModel
        {
            get
            {
                return (Tick)this.GetValue(MajorTickModelProperty);
            }
            set
            {
                this.SetValue(MajorTickModelProperty, value);
            }
        }
        static void MajorTickModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) 
        {
            var scale = d as ScaleView;
            var newModel = e.NewValue as Tick;
            var oldModel = e.OldValue as Tick;

            if (oldModel != null)
                oldModel.PropertyChanged -= scale.MajorTickModel_PropertyChanged;

            if (newModel != null)
                newModel.PropertyChanged += scale.MajorTickModel_PropertyChanged;

            scale.UpdateMajorTicks(true);
            scale.UpdateLabels(true);
        }

        void MajorTickModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Geometry"))
            {
                UpdateMajorTicks(true);
                UpdateLabels(false);
            }
        }

        void MinorTickModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Geometry"))
                UpdateMinorTicks(true);
        }

        public static readonly DependencyProperty MinorTickModelProperty = DependencyProperty.Register(
            "MinorTickModel", typeof(Tick), typeof(ScaleView), new PropertyMetadata(MinorTickModelChangedCallback));
        public Tick MinorTickModel
        {
            get
            {
                return (Tick)this.GetValue(MinorTickModelProperty);
            }
            set
            {
                this.SetValue(MinorTickModelProperty, value);
            }
        }
        static void MinorTickModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            var newModel = e.NewValue as Tick;
            var oldModel = e.OldValue as Tick;

            if (oldModel != null)
                oldModel.PropertyChanged -= scale.MinorTickModel_PropertyChanged;

            if (newModel != null)
                newModel.PropertyChanged += scale.MinorTickModel_PropertyChanged;

            scale.UpdateMinorTicks(true);
        }

        public static readonly DependencyProperty ScaleModelProperty = DependencyProperty.Register(
            "ScaleModel", typeof(Scale), typeof(ScaleView), new PropertyMetadata(ScaleModelChangedCallback));
        public Scale ScaleModel
        {
            get
            {
                return (Scale)this.GetValue(ScaleModelProperty);
            }
            set
            {
                this.SetValue(ScaleModelProperty, value);
            }
        }
        static void ScaleModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            var oldModel = e.OldValue as Scale;
            var newModel = e.NewValue as Scale;

            if (oldModel != null)
            {
                oldModel.LabelsUpdate -= scale.LabelsUpdate;
                oldModel.MinorTickUpdate -= scale.MinorTicksUpdate;
                oldModel.MajorTicksUpdate -= scale.MajorTicksUpdate;
            }

            if (newModel != null)
            {
                newModel.LabelsUpdate += scale.LabelsUpdate;
                newModel.MinorTickUpdate += scale.MinorTicksUpdate;
                newModel.MajorTicksUpdate += scale.MajorTicksUpdate;
            }

            scale.UpdateScale(true);
        }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(ScaleView), new PropertyMetadata(100.0, RadiusChangedCallback, RadiusCoerceCallback));
        public double Radius
        {
            get
            {
                return (double)this.GetValue(RadiusProperty);
            }
            set
            {
                this.SetValue(RadiusProperty, value);
            }
        }
        static void RadiusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            scale.UpdateScale(false);
        }
        static object RadiusCoerceCallback(DependencyObject d, object value)
        {
            double Value = (double)value;

            if ((Value <20) || double.IsNaN(Value) || double.IsInfinity(Value))
                Value = 20;

            return Value;
        }

        public static readonly DependencyProperty LabelPositionProperty = DependencyProperty.Register(
            "LabelPosition", typeof(Position), typeof(ScaleView), new PropertyMetadata(Position.Top, LabelPositionChangedCallback));
        public Position LabelPosition
        {
            get
            {
                return (Position)this.GetValue(LabelPositionProperty);
            }
            set
            {
                this.SetValue(LabelPositionProperty, value);
            }
        }
        static void LabelPositionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            scale.UpdateLabels(false); ;
        }



        public bool ShowLabels
        {
            get { return (bool)GetValue(ShowLabelsProperty); }
            set { SetValue(ShowLabelsProperty, value); }
        }

       
        public static readonly DependencyProperty ShowLabelsProperty = DependencyProperty.Register(
            "ShowLabels", typeof(bool), typeof(ScaleView), new PropertyMetadata(true, ShowLabelsChangedCallback));
        static void ShowLabelsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            scale.UpdateLabels(true);
        }

        

        public static readonly DependencyProperty MajorTickPositionProperty = DependencyProperty.Register(
            "MajorTickPosition", typeof(Position), typeof(ScaleView), new PropertyMetadata(Position.Top, MajorTickPositionChangedCallback));
        public Position MajorTickPosition
        {
            get
            {
                return (Position)this.GetValue(MajorTickPositionProperty);
            }
            set
            {
                this.SetValue(MajorTickPositionProperty, value);
            }
        }
        static void MajorTickPositionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            scale.UpdateLabels(false);
            scale.UpdateMajorTicks(false); 
        }

        public static readonly DependencyProperty MinorTickPositionProperty = DependencyProperty.Register(
            "MinorTickPosition", typeof(Position), typeof(ScaleView), new PropertyMetadata(Position.Top, MinorTickPositionChangedCallback));
        public Position MinorTickPosition
        {
            get
            {
                return (Position)this.GetValue(MinorTickPositionProperty);
            }
            set
            {
                this.SetValue(MinorTickPositionProperty, value);
            }
        }
        static void MinorTickPositionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            scale.UpdateMinorTicks(false);
        }

        public static readonly DependencyProperty RampFillProperty = DependencyProperty.Register(
            "RampFill", typeof(Brush), typeof(ScaleView), new PropertyMetadata(new SolidColorBrush( Colors.Black), RampFillChangedCallback));
        public Brush RampFill
        {
            get
            {
                return (Brush)this.GetValue(RampFillProperty);
            }
            set
            {
                this.SetValue(RampFillProperty, value);
            }
        }
        static void RampFillChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            scale.UpdateRamp();
        }

        public static readonly DependencyProperty RampThicknessProperty = DependencyProperty.Register(
            "RampThickness", typeof(double), typeof(ScaleView), new PropertyMetadata(0.5, RampThicknessChangedCallback));
        public double RampThickness
        {
            get
            {
                return (double)this.GetValue(RampThicknessProperty);
            }
            set
            {
                this.SetValue(RampThicknessProperty, value);
            }
        }
        static void RampThicknessChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            scale.UpdateRamp();
        }

        public static readonly DependencyProperty RampPositionProperty = DependencyProperty.Register(
            "RampPosition", typeof(Position), typeof(ScaleView), new PropertyMetadata(Position.Center, RampPositionChangedCallback));
        public Position RampPosition
        {
            get
            {
                return (Position)this.GetValue(RampPositionProperty);
            }
            set
            {
                this.SetValue(RampPositionProperty, value);
            }
        }
        static void RampPositionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scale = d as ScaleView;
            scale.UpdateRamp();
        }

        #endregion


        #region Methods

        void LabelsUpdate(object sender, LabelsUpdateArgs e)
        {
            UpdateLabels(false);
        }
        void MinorTicksUpdate(object sender, TicksUpdateArgs e)
        {
            UpdateMinorTicks(true);
        }
        void MajorTicksUpdate(object sender, TicksUpdateArgs e)
        {
            UpdateMajorTicks(false);
            UpdateRamp();
        }

        void UpdateScale(bool Forced )
        {

            UpdateMajorTicks(Forced);
            UpdateLabels(Forced);
            UpdateMinorTicks(Forced);
            UpdateRamp();
        }

        void UpdateMajorTicks(bool Forced)
        {
            if ((ScaleModel != null) && (MajorTickModel != null))
            {
                if ((ScaleModel.MajorTicks.Length != this.xamlMajorTicks.Count) || Forced)
                {
                    ClearXamlMajorTicks();
                    CreateXamlMajorTicks();

                    ScaleCanvas.UpdateLayout();
                }

                foreach (var xamlTick in xamlMajorTicks)
                    ConfigureMajorTick(xamlTick);
            }
            else
            {
                ClearXamlMajorTicks();
            }
        }
        void CreateXamlMajorTicks()
        {
            foreach (var modelTick in ScaleModel.MajorTicks)
            {
                var xamlTick = MajorTickModel.CreateTick();

                xamlTick.SetValue(Canvas.ZIndexProperty, 2);
                xamlTick.DataContext = modelTick;

                xamlMajorTicks.Add(xamlTick);

                ScaleCanvas.Children.Add(xamlTick);
            }
        }
        void ClearXamlMajorTicks()
        {
            foreach (var xamlTick in this.xamlMajorTicks)
                ScaleCanvas.Children.Remove(xamlTick);

            this.xamlMajorTicks.Clear();
        }
        protected abstract void ConfigureMajorTick(FrameworkElement Tick);

        void UpdateLabels(bool Forced)
        {
            if ( (ScaleModel != null) && (MajorTickModel != null) && (ShowLabels) )
            {

                if ((ScaleModel.Labels.Length != this.xamlLabels.Count) || Forced)
                {
                    ClearXamlLabels();
                    CreateXamlLabels();
                }
                else
                    for (int i = 0; i < xamlLabels.Count; i++)
                        ((TextBlock)xamlLabels[i]).Text = ScaleModel.Labels[i].String;

                ScaleCanvas.UpdateLayout();

                foreach (var xamlLabel in xamlLabels)
                    ConfigureXamlLabel(xamlLabel);
            }
            else
            {
                ClearXamlLabels();
            }
            
        }
        private void CreateXamlLabels()
        {
            foreach (var modelLabel in ScaleModel.Labels)
            {
                TextBlock xamlLabel = new TextBlock();
                xamlLabel.TextAlignment = TextAlignment.Center;
                xamlLabel.VerticalAlignment = VerticalAlignment.Center;
                xamlLabel.HorizontalAlignment = HorizontalAlignment.Center;

                xamlLabel.Text = modelLabel.String;
                xamlLabel.DataContext = modelLabel;
                xamlLabel.SetValue(Canvas.ZIndexProperty, 3);

                xamlLabels.Add(xamlLabel);
                ScaleCanvas.Children.Add(xamlLabel);
            }
        }
        private void ClearXamlLabels()
        {
            foreach (var xamlLabel in this.xamlLabels)
                ScaleCanvas.Children.Remove(xamlLabel);

            this.xamlLabels.Clear();
        }
        protected abstract void ConfigureXamlLabel(FrameworkElement label);

        void UpdateMinorTicks(bool Forced)
        {
            if ( (ScaleModel != null) && (MinorTickModel != null) )
            {
                if ((ScaleModel.MinorTicks.Length != this.xamlMinorTicks.Count) || Forced)
                {
                    ClearXamlMinorTicks();
                    CreateXamlMinorTicks();

                    ScaleCanvas.UpdateLayout();
                }


                foreach (var xamlTick in xamlMinorTicks)
                    ConfigureMinorTick(xamlTick);
            }
            else
            {
                ClearXamlMinorTicks();
            }
        }
        void CreateXamlMinorTicks()
        {
            foreach (var modelTick in ScaleModel.MinorTicks)
            {
                var xamlTick = MinorTickModel.CreateTick();

                xamlTick.SetValue(Canvas.ZIndexProperty, 1);
                xamlTick.DataContext = modelTick;

                xamlMinorTicks.Add(xamlTick);
                ScaleCanvas.Children.Add(xamlTick);
            }
        }
        void ClearXamlMinorTicks()
        {
            foreach (var xamlTick in this.xamlMinorTicks)
                ScaleCanvas.Children.Remove(xamlTick);

            this.xamlMinorTicks.Clear();
        }
        protected abstract void ConfigureMinorTick(FrameworkElement Tick);



        void UpdateRamp()
        {
            if (xamlRamp != null)
                ScaleCanvas.Children.Remove(xamlRamp);

            if (ScaleModel != null)
            {
                xamlRamp = CreateRamp();
                ScaleCanvas.Children.Add(xamlRamp);
            }
        }


        protected abstract FrameworkElement CreateRamp();

        #endregion Methods
    }
}
