using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArtForEveryone
{
    // klasa odpowiadająca za odcinki
    internal class LineSegment : DrawingHelper
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        private Point draggingPoint { get; set; }

        public Line Line { get; set; }
        public RoutedEventHandler MoveMenuItem_Click { get; private set; }

        public LineSegment(Point startPoint, Point endPoint, Canvas canvas, Color color)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            InitializeLine(canvas, color);
            DrawPoints();
        }

        private void InitializeLine(Canvas canvas, Color color)
        {
            Brush brushColor = new SolidColorBrush(color);
            Line = new Line
            {
                X1 = StartPoint.X,
                Y1 = StartPoint.Y,
                X2 = EndPoint.X,
                Y2 = EndPoint.Y,
                Stroke = brushColor,
                StrokeThickness = 1.5
            };
            canvas.Children.Add(Line);
        }

        public void DrawPoints()
        {
            //  funkcja z DrawingHelper do rysowania punktów 
            Ellipse startDot = DrawPoint(5.0, StartPoint.X, StartPoint.Y, Colors.Pink, (Canvas)Line.Parent);
            Ellipse endDot = DrawPoint(5.0, EndPoint.X, EndPoint.Y, Colors.Pink, (Canvas)Line.Parent);
        }

        public void MoveSelectedPoint(ref Point currentPoint, Canvas canvas)
        {

            // Przesuń punkt do nowej pozycji
            if (draggingPoint == StartPoint)
            {
                StartPoint = currentPoint;
                draggingPoint = StartPoint;
            }
            else if (draggingPoint == EndPoint)
            {
                EndPoint = currentPoint;
                draggingPoint = EndPoint;
            }

                // Przesuń odcinek do nowych pozycji punktów
            Line.X1 = StartPoint.X;
            Line.Y1 = StartPoint.Y;
            Line.X2 = EndPoint.X;
            Line.Y2 = EndPoint.Y;
            //DrawPoints();




            // Usuń dotychczasową linię
            //this.Line = null;
            //InitializeLine(canvas);
        }


        public void selectedLineSegment(object sender, MouseButtonEventArgs e)
        {
            // Sprawdź, który punkt jest bliżej kliknięcia
            Point clickedPoint = e.GetPosition((IInputElement)sender);
            double distanceToStart = (clickedPoint - StartPoint).Length;
            double distanceToEnd = (clickedPoint - EndPoint).Length;

            // Zaznacz punkt bliższy do kliknięcia
            if (distanceToStart < distanceToEnd)
            {
                SelectPoint(StartPoint);
                draggingPoint = StartPoint;
            }
            else
            {
                SelectPoint(EndPoint);
                draggingPoint = EndPoint;
            }
            Line.Stroke = new SolidColorBrush(Colors.Magenta);
        }

        private void SelectPoint(Point point)
        {
            // zmiana wybranego punktu
            

            DrawPoint(8.0, point.X, point.Y, Colors.Blue, (Canvas)Line.Parent);
        }

 

     

   
        //TODO
        public Rect GetBounds()
        {
            // TODO !!!!!!!!!!!!!!
            // Zakładam, że GetBounds to metoda zwracająca prostokąt obejmujący odcinek
            //return new Rect(StartPoint, EndPoint);

            double halfStrokeThickness = Line.StrokeThickness / 2.0;
            // Wyznacz kierunek odcinka
            Vector direction = new Vector(EndPoint.X - StartPoint.X, EndPoint.Y - StartPoint.Y);
            direction.Normalize();

            // Wektor prostopadły do odcinka
            Vector perpendicular = new Vector(-direction.Y, direction.X);

            // Przesunięcie o połowę grubości linii w obie strony prostopadle do odcinka
            Vector offset = halfStrokeThickness * perpendicular;

            Point topLeft = new Point(Math.Min(StartPoint.X, EndPoint.X), Math.Min(StartPoint.Y, EndPoint.Y));
            Point bottomRight = new Point(Math.Max(StartPoint.X, EndPoint.X), Math.Max(StartPoint.Y, EndPoint.Y));

            // Dodaj przesunięcie
            topLeft += offset;
            bottomRight += offset;

            return new Rect(topLeft, bottomRight);
        }


    }
}
