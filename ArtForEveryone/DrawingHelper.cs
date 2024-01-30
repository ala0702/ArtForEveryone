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
    //
    internal class DrawingHelper
    {

        // DRAWING POINT ?ELIPSES?
        public Ellipse DrawPoint(double size, double X, double Y, Color color, Canvas canvas)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = size;
            ellipse.Height = size;

            Canvas.SetTop(ellipse, Y - ellipse.Height / 2);
            Canvas.SetLeft(ellipse, X - ellipse.Width / 2);

            Brush brushColor = new SolidColorBrush(color);

            ellipse.Fill = brushColor;

            canvas.Children.Add(ellipse);

            return ellipse;
        }

        // DRAWING CUSTOM LINES
        public Line DrawCustomLine(ref Point currentPoint, MouseEventArgs e, Canvas canvas, Color color)
        {
            Line line = new Line();
            Brush brushColor = new SolidColorBrush(color);
            line.Stroke = brushColor;
            line.StrokeThickness = 2; 

         
            line.X1 = currentPoint.X;
            line.Y1 = currentPoint.Y;
            line.X2 = e.GetPosition(canvas).X;
            line.Y2 = e.GetPosition(canvas).Y;

            currentPoint = e.GetPosition(canvas);

            canvas.Children.Add(line);

            return line;
        }

        public Line DrawStreightLine(Point startPoint, Point currentPoint, Canvas canvas, Color color)
        {
            Line line = new Line();
            Brush brushColor = new SolidColorBrush(color);
            line.Stroke = brushColor;
            line.StrokeThickness = 2;
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = currentPoint.X;
            line.Y2 = currentPoint.Y;
            line.IsHitTestVisible = true;
            canvas.Children.Add(line);

            return line;
        }
        
        public void DrawHexagon(Point currentPoint, Canvas canvas, Color color)
        {
            double mouseX = currentPoint.X;
            double mouseY = currentPoint.Y;
            double polySize = 40;
           
            //  polygon to represent a circle
            Polygon poly = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            poly.Stroke =brushColor; 
            poly.StrokeThickness = 2;   

            int sides = 6; 
            double angleIncrement = 360.0 / sides;

            for (int i = 0; i < sides; i++)
            {
                double angle = i * angleIncrement;
                double x = mouseX + polySize * Math.Cos(Math.PI * angle / 180.0); //punkty na kole
                double y = mouseY + polySize * Math.Sin(Math.PI * angle / 180.0);
                poly.Points.Add(new Point(x, y));
            }
            canvas.Children.Add(poly);
        }

        public void DrawPentagon(Point currentPoint, Canvas canvas, Color color)
        {
            double mouseX = currentPoint.X;
            double mouseY = currentPoint.Y;
            double polySize = 40;


            Polygon poly = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            poly.Stroke = brushColor; 
            poly.StrokeThickness = 2;    

            int sides = 5; 
            double angleIncrement = 360.0 / sides;

            for (int i = 0; i < sides; i++)
            {
                double angle = i * angleIncrement;
                double x = mouseX + polySize * Math.Cos(Math.PI * angle / 180.0);
                double y = mouseY + polySize * Math.Sin(Math.PI * angle / 180.0);
                poly.Points.Add(new Point(x, y));
            }

            
            canvas.Children.Add(poly);
        }

        public void DrawTriangle(Point currentPoint, Canvas canvas, Color color)
        {
            double mouseX = currentPoint.X;
            double mouseY = currentPoint.Y;
            double polySize = 40;

            Polygon poly = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            poly.Stroke = brushColor; 
            poly.StrokeThickness = 2;  

           
            int sides = 3; 
            double angleIncrement = 360.0 / sides;

            for (int i = 0; i < sides; i++)
            {
                double angle = i * angleIncrement;
                double x = mouseX + polySize * Math.Cos(Math.PI * angle / 180.0);
                double y = mouseY + polySize * Math.Sin(Math.PI * angle / 180.0);
                poly.Points.Add(new Point(x, y));
            }

            canvas.Children.Add(poly);
        }

        public void DrawDecagon(Point currentPoint, Canvas canvas, Color color)
        {
            double mouseX = currentPoint.X;
            double mouseY = currentPoint.Y;
            double polySize = 40;

            Polygon poly = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            poly.Stroke = brushColor;
            poly.StrokeThickness = 2;  

            int sides = 10;
            double angleIncrement = 360.0 / sides;

            for (int i = 0; i < sides; i++)
            {
                double angle = i * angleIncrement;
                double x = mouseX + polySize * Math.Cos(Math.PI * angle / 180.0);
                double y = mouseY + polySize * Math.Sin(Math.PI * angle / 180.0);
                poly.Points.Add(new Point(x, y));
            }

            canvas.Children.Add(poly);
        }
        public void DrawSquare(Point currentPoint, Canvas canvas, Color color)
        {
            double mouseX = currentPoint.X;
            double mouseY = currentPoint.Y;
            double polySize = 40;


            Polygon poly = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            poly.Stroke = brushColor;
            poly.StrokeThickness = 2;   

            int sides = 4;
            double angleIncrement = 360.0 / sides;

            for (int i = 0; i < sides; i++)
            {
                double angle = i * angleIncrement;
                double x = mouseX + polySize * Math.Cos(Math.PI * angle / 180.0);
                double y = mouseY + polySize * Math.Sin(Math.PI * angle / 180.0);
                poly.Points.Add(new Point(x, y));
            }

            canvas.Children.Add(poly);
        }

        public void DrawParallelogram(Point currentPoint, Canvas canvas, Color color, double width = 50, double height = 30)
        {
            double centerX = currentPoint.X;
            double centerY = currentPoint.Y;

            Polygon poly = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            poly.Stroke = brushColor; 
            poly.StrokeThickness = 2;

            int sides = 4;
            double angleIncrement = 360.0 / sides;

            for (int i = 0; i < sides; i++)
            {
                double angle = i * angleIncrement;
                double x = centerX + width / 2 * Math.Cos(Math.PI * angle / 180.0);
                double y = centerY + height / 2 * Math.Sin(Math.PI * angle / 180.0);
                poly.Points.Add(new Point(x, y));
            }

            canvas.Children.Add(poly);
        }

        public void DrawRectangle(Point currentPoint, Canvas canvas, Color color, double width = 80, double height = 20)
        {
            double centerX = currentPoint.X;
            double centerY = currentPoint.Y;

            Polygon poly = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            poly.Stroke = brushColor; 
            poly.StrokeThickness = 2;

            poly.Points.Add(new Point(centerX - width / 2, centerY - height / 2)); // Lewy górny róg
            poly.Points.Add(new Point(centerX + width / 2, centerY - height / 2)); // Prawy górny róg
            poly.Points.Add(new Point(centerX + width / 2, centerY + height / 2)); // Prawy dolny róg
            poly.Points.Add(new Point(centerX - width / 2, centerY + height / 2)); // Lewy dolny róg

            canvas.Children.Add(poly);
        }

        public void DrawStar(Point center, Canvas canvas, Color color, double radius = 20, int arms = 8)
        {
            Polygon star = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            star.Stroke = brushColor; 
            star.StrokeThickness = 2;

            double angleIncrement = 360.0 / (2 * arms); 

            for (int i = 0; i < 2 * arms; i++)
            {
                double angle = i * angleIncrement;

                //  dłuższe i krótsze linie, aby uzyskać efekt gwiazdy
                double armRadius = (i % 2 == 0) ? radius : radius / 2;

                double x = center.X + armRadius * Math.Cos(Math.PI * angle / 180.0);
                double y = center.Y + armRadius * Math.Sin(Math.PI * angle / 180.0);

                star.Points.Add(new Point(x, y));
            }

            canvas.Children.Add(star);
        }

        public void DrawArrow(Point startPoint, Canvas canvas, Color color, double angleInDegrees = 120, double length = 80)
        {
            Polygon arrow = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            arrow.Stroke = brushColor; 
            arrow.StrokeThickness = 2;

            double arrowAngle = 30.0; // kat pomiędzy ramionami strzałki

            double angleInRadians = Math.PI * angleInDegrees / 180.0;

            double endX = startPoint.X + length * Math.Cos(angleInRadians);
            double endY = startPoint.Y + length * Math.Sin(angleInRadians);

            arrow.Points.Add(new Point(startPoint.X, startPoint.Y)); 

            for (int i = -1; i <= 1; i += 2)
            {
                double arrowTipX = endX + i * arrowAngle * Math.Cos(angleInRadians - Math.PI / 2);
                double arrowTipY = endY + i * arrowAngle * Math.Sin(angleInRadians - Math.PI / 2);

                arrow.Points.Add(new Point(arrowTipX, arrowTipY));
            }

            arrow.Points.Add(new Point(endX, endY)); 

            canvas.Children.Add(arrow);
        }

        public void DrawVerticalArrow(Point bottomPoint, Canvas canvas, Color color, double angleInDegrees = 180, double length = 50)
        {
            Polygon arrow = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            arrow.Stroke = brushColor; 
            arrow.StrokeThickness = 2;

            double arrowAngle = 30.0; 

            double angleInRadians = Math.PI * angleInDegrees / 180.0;

        
            double topX = bottomPoint.X + length * Math.Cos(angleInRadians);
            double topY = bottomPoint.Y - length * Math.Sin(angleInRadians); // Uwaga: Odejmujemy, aby iść w górę

            arrow.Points.Add(new Point(bottomPoint.X, bottomPoint.Y)); 

            for (int i = -1; i <= 1; i += 2)
            {
                double arrowTipX = topX + i * arrowAngle * Math.Cos(angleInRadians - Math.PI / 2);
                double arrowTipY = topY + i * arrowAngle * Math.Sin(angleInRadians - Math.PI / 2);

                arrow.Points.Add(new Point(arrowTipX, arrowTipY));
            }

            arrow.Points.Add(new Point(topX, topY)); // Koniec strzałki (góra)

            canvas.Children.Add(arrow);
        }


        public void DrawCircle(Point currentPoint, Canvas canvas, Color color)
        {
            double mouseX = currentPoint.X;
            double mouseY = currentPoint.Y;
            double polySize = 40;

           
            Polygon poly = new Polygon();
            Brush brushColor = new SolidColorBrush(color);
            poly.Stroke = brushColor; 
            poly.StrokeThickness = 2;    

            int sides = 203; 
            double angleIncrement = 360.0 / sides;

            for (int i = 0; i < sides; i++)
            {
                double angle = i * angleIncrement;
                double x = mouseX + polySize * Math.Cos(Math.PI * angle / 180.0);
                double y = mouseY + polySize * Math.Sin(Math.PI * angle / 180.0);
                poly.Points.Add(new Point(x, y));
            }

            canvas.Children.Add(poly);
        }
    }
}
