using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.ComponentModel;

namespace LabControls.Common
{
    /// <summary>
    /// Логика взаимодействия для RoundPointerView.xaml
    /// </summary>
    public partial class RoundPointerView : UserControl, INotifyPropertyChanged
    {
        public RoundPointerView()
        {
            InitializeComponent();
        }

        void ScaleModel_ScaleUpdate(object sender, System.EventArgs e)
        {
            UpdateValue();
        }


        #region Dependency Properties

        public static readonly DependencyProperty PointerModelProperty = DependencyProperty.Register(
            "PointerModel", typeof(Pointer), typeof(RoundPointerView), new PropertyMetadata(PointerModelChangedCallback));
        public Pointer PointerModel
        {
            get { return (Pointer)GetValue(PointerModelProperty); }
            set { SetValue(PointerModelProperty, value); }
        }
        static void PointerModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as RoundPointerView;
            model.UpdatePointer();
        }

        public static readonly DependencyProperty ScaleModelProperty = DependencyProperty.Register(
            "ScaleModel", typeof(Scale), typeof(RoundPointerView), new PropertyMetadata(ScaleModelChangedCallback));
        public Scale ScaleModel
        {
            get { return (Scale)GetValue(ScaleModelProperty); }
            set { SetValue(ScaleModelProperty, value); }
        }
        static void ScaleModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as RoundPointerView;
            var oldScale = e.OldValue as Scale;
            var newScale = e.NewValue as Scale;

            if (oldScale != null)
                oldScale.ScaleUpdate -= model.ScaleModel_ScaleUpdate;

            if (newScale != null)
                newScale.ScaleUpdate += model.ScaleModel_ScaleUpdate;

            model.UpdateValue();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(RoundPointerView), new PropertyMetadata(0.0, ValueChangedCallback));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as RoundPointerView;
            p.UpdateValue();
        }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(RoundPointerView), new PropertyMetadata(10.0, RadiusChangedCallback));
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        static void RadiusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as RoundPointerView;
            p.UpdateTransform();
        }

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(double), typeof(RoundPointerView), new PropertyMetadata(0.5, OffsetChangedCallback));
        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }
        static void OffsetChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as RoundPointerView;
            p.UpdateTransform();
        }

        #endregion

        #region Properties

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

        FrameworkElement CreatePointer()
        {
            var pointer = PointerModel.CreatePointer();

            var translate = new TranslateTransform();
            var rotate = new RotateTransform();
            var group = new TransformGroup();

            group.Children.Add(translate);
            group.Children.Add(rotate);


            var XBinding = new Binding("XShift");
            XBinding.Source = this;
            BindingOperations.SetBinding(translate, TranslateTransform.XProperty, XBinding);

            var YBinding = new Binding("YShift");
            YBinding.Source = this;
            BindingOperations.SetBinding(translate, TranslateTransform.YProperty, YBinding);

            var AngleBinding = new Binding("Angle");
            AngleBinding.Source = this;
            BindingOperations.SetBinding(rotate, RotateTransform.AngleProperty, AngleBinding);

            pointer.RenderTransform = group;

            return pointer;
        }

        void UpdatePointer()
        {
            pointerCanvas.Children.Clear();

            if (PointerModel != null)
            {
                pointerCanvas.Children.Add(CreatePointer());
                UpdateTransform();
            }


        }

        void UpdateTransform()
        {
            if (PointerModel != null)
            {

                XShift = 0;
                YShift = -Radius + Offset + PointerModel.Size.Height / 2;

                UpdateValue();
            }
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
