using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ArtForEveryone
{
    internal class CustomImage
    {
        public string ImagePath { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }

        public CustomImage(string imagePath, double coordinateX = 0, double coordinateY = 0, double width = 100, double height = 150)
        {
            ImagePath = imagePath;
            Width = width;
            Height = height;
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
        }

        public void AddToCanvas(Canvas canvas)
        {
            try
            {
                // Tworzy obiekt Image
                Image image = new Image();

                // Ustawia źródło obrazka
                BitmapImage bitmap = new BitmapImage(new Uri(ImagePath));
                image.Source = bitmap;

                // Ustawia wymiary obrazka
                image.Width = Width;
                image.Height = Height;

                // Przykładowe ustawienia renderowania
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Top;


                // Ustawia pozycję obrazka na Canvas
                Canvas.SetLeft(image, CoordinateX);
                Canvas.SetTop(image, CoordinateY);

                // Dodaje obrazek do Canvas
                canvas.Children.Add(image);
            }
            catch (Exception ex)
            {
                // Obsługa błędów ładowania obrazka
                Console.WriteLine($"Błąd wczytywania obrazka: {ex.Message}");
            }
        }

        public void Resize(double newWidth, double newHeight)
        {
            Width = newWidth;
            Height = newHeight;
        }


    }
}
