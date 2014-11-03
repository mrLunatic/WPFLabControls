using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LabControls.Common
{
    public enum ShapeType
    {
        Rectangle,
        Triangle,
        InvTriangle,
        Ellipse
    }

    internal class Utilities
    {
        public static Shape CreateShape(ShapeType Type, Size Size, Color Stroke, double Thickness, Color Fill )
        {
            Shape shape = null;

            switch (Type)
            {
                case Common.ShapeType.Rectangle: shape = CreateRectangleShape(Size.Width, Size.Height); break;
                case Common.ShapeType.Triangle: shape = CreateTriangleShape(Size.Width, Size.Height); break;
                case Common.ShapeType.InvTriangle: shape = CreateInvTriangleShape(Size.Width, Size.Height); break;
                case Common.ShapeType.Ellipse: shape = CreateEllipseShape(Size.Width, Size.Height); break;
            }

            shape.Fill = new SolidColorBrush(Fill);
            shape.Stroke = new SolidColorBrush(Stroke);
            shape.StrokeThickness = Thickness;

            return shape;
            
        }

        static Shape CreateRectangleShape(double Width, double Height)
        {
            var rect = new Rectangle();

            rect.Width = Width;
            rect.Height = Height;

            return rect;
        }
        static Shape CreateTriangleShape(double Width, double Height)
        {
            var poly = new Polygon();

            poly.Points.Add(new Point(0, 0));
            poly.Points.Add(new Point(Width, 0));
            poly.Points.Add(new Point(Width / 2, Height));

            return poly;
        }
        static Shape CreateInvTriangleShape(double Width, double Height)
        {
            var poly = new Polygon();

            poly.Points.Add(new Point(Width / 2, 0));
            poly.Points.Add(new Point(Width, Height));
            poly.Points.Add(new Point(0, Height));

            return poly;
        }
        static Shape CreateEllipseShape(double Width, double Height)
        {
            var ell = new Ellipse();

            ell.Width = Width;
            ell.Height = Height;

            return ell;
        }

        public static Geometry CreateGeometry(ShapeType Type, Size Size)
        {
            Geometry geom = null;

            switch (Type)
            {
                case Common.ShapeType.Rectangle:
                    geom = CreateRectangleGeometry(Size.Width, Size.Height);
                    break;
                case Common.ShapeType.Triangle:
                    geom = CreateTriangeGeometry(Size.Width, Size.Height);
                    break;
                case Common.ShapeType.InvTriangle:
                    geom = CreateInvTriangeGeometry(Size.Width, Size.Height);
                    break;
                case Common.ShapeType.Ellipse:
                    geom = CreateEllipseGeometry(Size.Width, Size.Height);
                    break;
            }
            return geom;

        }

        static Geometry CreateRectangleGeometry(double Width, double Height)
        {
            return new RectangleGeometry(new Rect(-Width/2, -Height/2, Width, Height));
        }
        static Geometry CreateTriangeGeometry(double Width, double Height)
        {
            var points = new Point[] { new Point(Width/2, -Height/2), new Point(0, Height/2) };
            var segments = new PathSegment[] { new PolyLineSegment( points, true)};
            var figure = new PathFigure(new Point(-Width/2, -Height/2), segments, true);
            var geometry = new PathGeometry(new PathFigure[] { figure });

            return geometry;
        }
        static Geometry CreateInvTriangeGeometry(double Width, double Height)
        {
            var points = new Point[] { new Point(Width/2, Height/2), new Point(-Width/2, Height/2) };
            var segments = new PathSegment[] { new PolyLineSegment(points, true) };
            var figure = new PathFigure(new Point(0, -Height/2), segments, true);
            var geometry = new PathGeometry(new PathFigure[] { figure });

            return geometry;
        }
        static Geometry CreateEllipseGeometry(double Width, double Height)
        {
            return new EllipseGeometry(new Rect(-Width/2, -Height/2, Width, Height));
        }
        
        public static Geometry CreateArc(double StartAngle, double EndAngle, double Radius)
        {
            if (Radius < 0)
                Radius = 1;

            var startAngleRad = StartAngle * Math.PI / 180;
            var endAngleRad = EndAngle * Math.PI / 180;

            var startPoint = new Point(Radius * Math.Sin(startAngleRad), -Radius * Math.Cos(startAngleRad));
            var endPoint = new Point(Radius * Math.Sin(endAngleRad), -Radius * Math.Cos(endAngleRad));

            var size = new Size(Radius, Radius);
            var isLarge = (endAngleRad - startAngleRad) >= Math.PI;

            var direction = SweepDirection.Clockwise;

            if (startAngleRad > endAngleRad)
                direction = SweepDirection.Counterclockwise;

            var arcSegment = new ArcSegment(endPoint, size, 0, isLarge, direction, true);
            var arcFigure = new PathFigure(startPoint, new PathSegment[] { arcSegment }, false);
            var arcGeometry = new PathGeometry(new PathFigure[] { arcFigure });

            return arcGeometry;
        }

        public static Geometry CreateLine(double X1, double Y1, double X2, double Y2)
        {
            var startPoint = new Point(X1, Y1);
            var endPoint = new Point(X2, Y2);

            var lineSegment = new LineSegment(endPoint, true);
            var arcFigure = new PathFigure(startPoint, new PathSegment[] { lineSegment }, false);
            var arcGeometry = new PathGeometry(new PathFigure[] { arcFigure });

            return arcGeometry;
        }
    }


}
