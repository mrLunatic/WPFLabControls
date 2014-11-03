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
    public class RoundScaleView : ScaleView
    {
        protected override FrameworkElement CreateRamp()
        {
            var rampRadius = 0.0;

            switch (RampPosition)
            {
                case Position.Top:
                    rampRadius = Radius + RampThickness / 2;
                    break;
                case Position.Center:
                    rampRadius = Radius;
                    break;
                case Position.Bottom:
                    rampRadius = Radius - RampThickness / 2;
                    break;
            }

            var arcGeometry = Utilities.CreateArc(ScaleModel.ScaledMin, ScaleModel.ScaledMax, rampRadius);
            var arcPath = new Path();
            arcPath.Stroke = RampFill;
            arcPath.StrokeThickness = RampThickness;
            arcPath.Data = arcGeometry;

            arcPath.SetValue(Canvas.ZIndexProperty, -1);


            return arcPath;
        }
        protected override void ConfigureMinorTick(FrameworkElement Tick)
        {
            var Group = new TransformGroup();

            var Translation = new TranslateTransform();
            Group.Children.Add(Translation);

            var Rotation = new RotateTransform();
            Group.Children.Add(Rotation);

            var scaleTick = Tick.DataContext as ScaleTick;

            if (scaleTick == null)
                return;

            Translation.X = 0;

            switch (MinorTickPosition)
            {
                case Position.Top:
                    Translation.Y = -Radius - MinorTickModel.Size.Height / 2;
                    break;
                case Position.Center:
                    Translation.Y = -Radius;
                    break;
                case Position.Bottom:
                    Translation.Y = -Radius + MinorTickModel.Size.Height / 2;
                    break;
            }


            Rotation.Angle = ScaleModel.ScaleForward(scaleTick.Value);

            Tick.RenderTransform = Group;

        }
        protected override void ConfigureXamlLabel(FrameworkElement label)
        {
            var Transform = new TranslateTransform();
            var modelLabel = label.DataContext as ScaleLabel;

            if (modelLabel == null)
                return;

            var RadX = Radius;
            var RadY = Radius;

            double shift = 0.0;

            if (MajorTickModel != null)
            {
                shift = MajorTickModel.Size.Height;
            }
            switch (LabelPosition)
            {
                case Position.Top:
                    RadX += shift + label.ActualWidth / 2 + 10;
                    RadY += shift + label.ActualHeight / 2 + 5;
                    break;
                case Position.Bottom:
                    RadX -= shift + label.ActualWidth / 2 + 10;
                    RadY -= shift + label.ActualHeight / 2 + 5; ;
                    break;
                case Position.Center: break;
            }

            var Angle = ScaleModel.ScaleForward(modelLabel.Value) * Math.PI / 180;

            Transform.X = RadX * Math.Sin(Angle) - label.ActualWidth / 2;
            Transform.Y = -RadY * Math.Cos(Angle) - label.ActualHeight / 2;

            label.RenderTransform = Transform;

        }
        protected override void ConfigureMajorTick(FrameworkElement Tick)
        {
            var Group = new TransformGroup();

            var Translation = new TranslateTransform();
            Group.Children.Add(Translation);

            var Rotation = new RotateTransform();
            Group.Children.Add(Rotation);

            var scaleTick = Tick.DataContext as ScaleTick;

            if (scaleTick == null)
                return;

            Translation.X = 0;


            switch (MajorTickPosition)
            {
                case Position.Top:
                    Translation.Y = -Radius - MajorTickModel.Size.Height / 2;
                    break;
                case Position.Center:
                    Translation.Y = -Radius;
                    break;
                case Position.Bottom:
                    Translation.Y = -Radius + MajorTickModel.Size.Height / 2;
                    break;
            }

            Rotation.Angle = ScaleModel.ScaleForward(scaleTick.Value);

            Tick.RenderTransform = Group;

        }
    }
}
