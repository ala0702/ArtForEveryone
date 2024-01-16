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

namespace ArtForEveryone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        


        //Color ViewModel.MainColor = Colors.Red;

        int drawStyle = 0;

        Point currentPoint = new Point();
        DrawingHelper drawingHelper = new DrawingHelper();

        private List<LineSegment> lineSegments = new List<LineSegment>();

        Point startPoint = new Point();
        bool isStartPoint = false;
        Line previewLine = null;
        Ellipse dotStart = null;

        bool isEditingSegment = false;
        LineSegment clickedLineSegment = null;
  

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            this.DataContext = ViewModel;
            
        }


       
        private void WorkingSpace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(this);

                if (drawStyle == 2)
                {
                    drawingHelper.DrawPoint(6, currentPoint.X, currentPoint.Y,ViewModel.MainColor, WorkingSpace);
                }
                else if (drawStyle == 3)
                {
                    if (isStartPoint == false)
                    {
                        isStartPoint = true;
                        startPoint = currentPoint;
                        dotStart = drawingHelper.DrawPoint(6, startPoint.X, startPoint.Y, Colors.Magenta, WorkingSpace);
                    }
                    else
                    {
                        LineSegment newLine = new LineSegment(startPoint, e.GetPosition(this), WorkingSpace, ViewModel.MainColor);
                        lineSegments.Add(newLine);

                        isStartPoint = false;

                        if (previewLine != null)
                        {
                            WorkingSpace.Children.Remove(previewLine);
                            previewLine = null;
                        }
                        if (dotStart != null)
                        {
                            WorkingSpace.Children.Remove(dotStart);
                            dotStart = null;

                        }


                    }
                }
                else if (drawStyle == 4)
                {
                    if (isEditingSegment == false)
                    {
                        // Pobierz element pod kursorem myszy
                        var element = e.Source as UIElement;

                        // Sprawdź, czy element to Canvas
                        if (element is Canvas)
                        {
                            // Sprawdź, czy Canvas zawiera odcinek zaznaczony jako Hover
                            clickedLineSegment = GetHoveredLineSegment((Canvas)element, e.GetPosition((Canvas)element));

                            if (clickedLineSegment != null)
                            {
                                // Tutaj możesz wyświetlić numer klikniętego odcinka
                                int segmentIndex = lineSegments.IndexOf(clickedLineSegment);
                                //MessageBox.Show($"Kliknięty odcinek o numerze: {segmentIndex + 1}");

                                clickedLineSegment.selectedLineSegment(sender, e);
                                isEditingSegment = true;

                            }
                        }
                    }
                    else
                    {
                        isEditingSegment = false;
                        Brush brushColor = new SolidColorBrush(ViewModel.MainColor);
                        clickedLineSegment.Line.Stroke = brushColor;
                        clickedLineSegment.DrawPoints();
                    }
                }
                else if(drawStyle == 5)
                {
                    drawingHelper.DrawCircle(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 6)
                {
                    drawingHelper.DrawTriangle(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 7)
                {
                    drawingHelper.DrawSquare(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 8)
                {
                    //prostokat
                    drawingHelper.DrawRectangle(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 9)
                {

                    drawingHelper.DrawPentagon(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 10)
                {
                    drawingHelper.DrawHexagon(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 11)
                {
                    drawingHelper.DrawDecagon(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 12)
                {
                    //eownoleglobok
                    drawingHelper.DrawParallelogram(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 13)
                {
                    //eownoleglobok
                    drawingHelper.DrawStar(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
                else if (drawStyle == 14)
                {
                    //eownoleglobok
                    drawingHelper.DrawVerticalArrow(currentPoint, WorkingSpace, ViewModel.MainColor);
                }
            }
        }
        private void WorkingSpace_MouseMove(object sender, MouseEventArgs e)
        {
            currentPoint = e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (drawStyle == 1)
                {
                    // Use the returned line from DrawCustomLine and add it to the canvas
                    drawingHelper.DrawCustomLine(ref currentPoint, e, WorkingSpace, ViewModel.MainColor);
                    drawingHelper.DrawPoint(1.0, currentPoint.X, currentPoint.Y, ViewModel.MainColor, WorkingSpace);
                    // Narysuj nową tymczasową linię



                }
            }
            else if (e.LeftButton == MouseButtonState.Released)
            {
                if (drawStyle == 3 && isStartPoint)
                {
                    // jeśli masz już utworzoną tymczasową linię, usuń ją przed narysowaniem nowej
                    if (previewLine != null)
                    {
                        WorkingSpace.Children.Remove(previewLine);
                        previewLine = null;
                    }

                    // Narysuj nową tymczasową linię
                    previewLine = drawingHelper.DrawStreightLine(startPoint, e.GetPosition(this), WorkingSpace, ViewModel.MainColor);

                }
                else if (drawStyle == 4 && isEditingSegment)
                {
                    if (clickedLineSegment != null)
                    {
                        clickedLineSegment.MoveSelectedPoint(ref currentPoint, WorkingSpace);
                        //clickedLineSegment.selectedLineSegment(sender, (MouseButtonEventArgs)e);
                        //clickedLineSegment.UpdateLine(e.GetPosition(WorkingSpace));
                    }

                }
            }
        
            
        }

        // getting selected linesegmentfromlist
        private LineSegment GetHoveredLineSegment(Canvas canvas, Point mousePosition)
        {
            foreach (LineSegment lineSegment in lineSegments)
            {
                // Przyjmuję, że GetBounds to metoda zwracająca obszar objęty przez odcinek (mogła byś ją dodać do klasy LineSegment)
                Rect bounds = lineSegment.GetBounds();

                if (bounds.Contains(mousePosition))
                {
                    // Jeśli mysz znajduje się w obszarze odcinka, to zwróć ten odcinek
                    return lineSegment;
                }
            }
            return null;
        }



        // NAVBAR OBSLUGA
        private void Przycisk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Informacje o aplikacji");
        }

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            // przycisk "Rysuj dowolnie"
            drawStyle = 1;
        }

        private void DrawPoints_Click(object sender, RoutedEventArgs e)
        {
            // przycisk "Rysuj punkty"
            drawStyle = 2;
        }

        private void DrawSegmentBtn_Click(object sender, RoutedEventArgs e)
        {
            // przycisk "Rysuj odcinek"
            drawStyle = 3;
        
        }

        private void EditSegmentBtn_Click(object sender, RoutedEventArgs e)
        {
            // przycisk "Edytuj odcinek"
            drawStyle = 4;
        }

        private void DrawCircle_Click(object sender, RoutedEventArgs e)
        {

            drawStyle = 5;

        }

        private void DrawTriangle_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 6;
        }

        private void DrawSquare_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 7;
        }

        private void DrawRectangle_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 8;
        }

        private void DrawPentagon_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 9;
        }

        private void DrawHexagon_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 10;
        }

        private void DrawDecagon_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 11;
        }

        private void DrawParallelogram_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 12;
        }

        private void DrawStar_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 13;
        }

        private void DrawArrow_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 14;
        }

        private void ColorPicker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorPickerWindow colorPickerWindow = new ColorPickerWindow(ViewModel);
            colorPickerWindow.Show();
        }
    }
    
}
