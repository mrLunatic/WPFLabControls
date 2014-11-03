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
    public class HorisontalLinearScale : ScaleView
    {


        protected override void ConfigureMajorTick(System.Windows.FrameworkElement Tick)
        {
            var Group = new TransformGroup();

            var Translation = new TranslateTransform();
            Group.Children.Add(Translation);



            var scaleTick = Tick.DataContext as ScaleTick;

            if (scaleTick == null)
                return;

            Translation.X = ScaleModel.ScaleForward(scaleTick.Value);


            switch (MajorTickPosition)
            {
                case Position.Top:
                    Translation.Y =-  MajorTickModel.Size.Height / 2;
                    break;
                case Position.Center:
                    Translation.Y = 0;
                    break;
                case Position.Bottom:
                    Translation.Y =  MajorTickModel.Size.Height / 2;
                    break;
            }



            Tick.RenderTransform = Group;

            //throw new NotImplementedException();
        }

        protected override void ConfigureXamlLabel(System.Windows.FrameworkElement label)
        {
            var Transform = new TranslateTransform();
            var modelLabel = label.DataContext as ScaleLabel;


            if (modelLabel == null)
                return;

            double X = ScaleModel.ScaleForward(modelLabel.Value) - label.ActualWidth / 2;
            double Y = -label.ActualHeight/2;
            double YShift = 0;

            if (MajorTickModel != null)
            {
                YShift = MajorTickModel.Size.Height + label.ActualHeight / 2;
            }
            else
            {
                YShift = label.ActualHeight/2;
            }
            switch (LabelPosition)
            {
                case Position.Top:
                    Y -= YShift;
                    break;
                case Position.Bottom:
                    Y += YShift ;
                    break;
                case Position.Center:
                    break;
            }

           

            Transform.X = X;
            Transform.Y = Y;


            label.RenderTransform = Transform;
        }

        protected override void ConfigureMinorTick(System.Windows.FrameworkElement Tick)
        {
            var Group = new TransformGroup();

            var Translation = new TranslateTransform();
            Group.Children.Add(Translation);



            var scaleTick = Tick.DataContext as ScaleTick;

            if (scaleTick == null)
                return;

            Translation.X = ScaleModel.ScaleForward(scaleTick.Value);


            switch (MinorTickPosition)
            {
                case Position.Top:
                    Translation.Y = -MinorTickModel.Size.Height / 2;
                    break;
                case Position.Center:
                    Translation.Y = 0;
                    break;
                case Position.Bottom:
                    Translation.Y = MinorTickModel.Size.Height / 2;
                    break;
            }



            Tick.RenderTransform = Group;
        }

        protected override System.Windows.FrameworkElement CreateRamp()
        {
            var X1 = ScaleModel.ScaledMin;
            var X2 = ScaleModel.ScaledMax;


            var Y = 0.0;

            switch (RampPosition)
            {
                case Position.Top: Y -= RampThickness / 2; break;
                case Position.Bottom: Y += RampThickness / 2; break;
                case Position.Center: break;
            }

            var path = new Path();
            path.Stroke = RampFill;
            path.StrokeThickness = RampThickness;
            path.Data = Utilities.CreateLine(X1, Y, X2, Y);

            return path;
           
        }
    }
}
