using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ArtForEveryone
{
    public partial class ImageProcessingWindow : Window
    {
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        private Image<Bgr, byte> originalImage;

        public ImageProcessingWindow()
        {
            InitializeComponent();
        }

        private void UploadPhotoToFIlter_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                originalImage = new Image<Bgr, byte>(openDialog.FileName);

                // Zastosowanie filtru Sobel
                Mat sobelImage = new Mat();
                CvInvoke.CvtColor(originalImage, sobelImage, ColorConversion.Bgr2Gray);
                CvInvoke.Sobel(originalImage, sobelImage, DepthType.Cv8U, 1, 1,21);

                // Wyświetlenie obrazu i jego przetworzonej wersji
                image.Source = new BitmapImage(new Uri(openDialog.FileName));
               

                Bitmap IMG = sobelImage.ToBitmap();
                imageModified.Source = ImageSourceFromBitmap(IMG);
            }
        }

        private ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }




    }
}
