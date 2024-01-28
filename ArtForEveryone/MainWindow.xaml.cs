using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        //customline
        Point startCustomPoint = new Point();

        //odcinki
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
                else if(drawStyle == 15)
                {
                    var clickedElement = e.Source as FrameworkElement;

                    if(clickedElement != null)
                    {
                        if(WorkingSpace.Children.Contains(clickedElement))
                        {
                            WorkingSpace.Children.Remove(clickedElement);
                        }
                    }
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
                    //drawingHelper.DrawCustomLine(ref currentPoint, e, WorkingSpace, ViewModel.MainColor);

                    if (startCustomPoint == default(Point))
                    {
                        startCustomPoint = currentPoint; // Zainicjowanie punktu początkowego, jeśli nie został jeszcze ustawiony
                    }
                    currentPoint = e.GetPosition(WorkingSpace);

                    Line line = new Line();
                    Brush brushColor = new SolidColorBrush(ViewModel.MainColor);
                    line.Stroke = brushColor;
                    line.StrokeThickness = 2; // Grubość linii
                    line.X1 = startCustomPoint.X;
                    line.Y1 = startCustomPoint.Y;
                    line.X2 = currentPoint.X;
                    line.Y2 = currentPoint.Y;

                    WorkingSpace.Children.Add(line);

                    // Aktualizacja punktu początkowego na bieżącą pozycję myszy
                    startCustomPoint = currentPoint;

                }
                if (drawStyle == 15)
                {
                    var clickedElement = e.Source as FrameworkElement;

                    if (clickedElement != null)
                    {
                        if (WorkingSpace.Children.Contains(clickedElement))
                        {
                            WorkingSpace.Children.Remove(clickedElement);
                        }
                    }
                }
            }
            else if (e.LeftButton == MouseButtonState.Released)
            {
                if(drawStyle == 1)
                {
                    // resetowanie punkt  gdy puścimy lewy przycisk myszy
                    startCustomPoint = default(Point);
                }
                if (drawStyle == 3 && isStartPoint)
                {
                    // jeśli masz już utworzoną tymczasową linię, usuń ją przed narysowaniem nowej
                    if (previewLine != null)
                    {
                        WorkingSpace.Children.Remove(previewLine);
                        previewLine = null;
                    }

                    // narysuj nową tymczasową linię
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

        // chosing color picker
        private void ColorPicker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorPickerWindow colorPickerWindow = new ColorPickerWindow(ViewModel);
            colorPickerWindow.Show();
        }

        // save file button
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File (*.png)|*.png|Image FIle (*.jpg)|*.jpg";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == true)
            {
                Uri newFileUri = new Uri(saveFileDialog.FileName);
                SaveImageAsPng(newFileUri, WorkingSpace);
            }

        }

        private void SaveImageAsPng(Uri newFileUri, Canvas canvas)
        {
            if(newFileUri == null)
            {
                return;
            }
            Transform transform = canvas.LayoutTransform;
            canvas.LayoutTransform = null;
            Size size = new Size(canvas.Width, canvas.Height);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(canvas);
            using (FileStream outStream = new FileStream(newFileUri.LocalPath, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(outStream);
            }
            canvas.LayoutTransform = transform;
        }


        // loading image into canvas
        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;

                // Pytanie o wymiary
                AskForDimensions(out double width, out double height);
                AskForCoordinates(out double x, out double y);

                // Tworzy obiekt CustomImage i dodaje go do Canvas
                CustomImage customImage = new CustomImage(imagePath, x, y, width, height);
                customImage.AddToCanvas(WorkingSpace);
            }
        }
        private void AskForCoordinates(out double x, out double y)
        {
            InputDialog xDialog = new InputDialog("Podaj współrzędną x:");
            if (xDialog.ShowDialog() == true)
            {
                double.TryParse(xDialog.Result, out x);
                y = x;

                InputDialog yDialog = new InputDialog("Podaj współrzędną y:");
                if (yDialog.ShowDialog() == true)
                {
                    double.TryParse(yDialog.Result, out y);
                }
            }
            else
            {
                x = 0;
                y = 0;
            }

        }
        private void AskForDimensions(out double width, out double height)
        {
            InputDialog widthDialog = new InputDialog("Podaj szerokość obrazu:");
            if (widthDialog.ShowDialog() == true)
            {
                double.TryParse(widthDialog.Result, out width);
                height = width;

                InputDialog heightDialog = new InputDialog("Podaj wysokość obrazu:");
                if (heightDialog.ShowDialog() == true)
                {
                    double.TryParse(heightDialog.Result, out height);
                }
            }
            else
            {
                height = 0;
                width = 0; 
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ImageFilteringMaskWindow imageFIlteringMaskWindow = new ImageFilteringMaskWindow();
            imageFIlteringMaskWindow.Show();

        }

        private void btnErase_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 15;
        }

    

        private void btnSelectFiltr_Click(object sender, RoutedEventArgs e)
        {
            ImageProcessingWindow imageProcessingWindow = new ImageProcessingWindow();
            imageProcessingWindow.Show();
        }
    }
    
}
