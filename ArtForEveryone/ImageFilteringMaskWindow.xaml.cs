using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;

namespace ArtForEveryone
{
    /// <summary>
    /// Interaction logic for ImageFilteringMaskWindow.xaml
    /// </summary>
    public partial class ImageFilteringMaskWindow : Window
    {
        private Image<Bgr, byte> originalImage;

        public ImageFilteringMaskWindow()
        {
            InitializeComponent();
        }

        private void UploadPhotoToFIlter_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if(openDialog.ShowDialog() == true)
            {
                originalImage = new Image<Bgr, byte>(openDialog.FileName);
                image.Source = new BitmapImage(new Uri(openDialog.FileName));
            }

        }

        private void btnApplyMask_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdź, czy obraz został wczytany
            if (image.Source == null)
            {
                MessageBox.Show("Wczytaj obraz przed zastosowaniem maski.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Pobierz wartości z pól tekstowych
                double[] values = new double[9];
                values[0] = double.Parse(tb1.Text);
                values[1] = double.Parse(tb2.Text);
                values[2] = double.Parse(tb3.Text);
                values[3] = double.Parse(tb4.Text);
                values[4] = double.Parse(tb5.Text);
                values[5] = double.Parse(tb6.Text);
                values[6] = double.Parse(tb7.Text);
                values[7] = double.Parse(tb8.Text);
                values[8] = double.Parse(tb9.Text);

                // Utwórz maskę filtracji na podstawie pobranych wartości
                Mat kernel = new Mat(3, 3, DepthType.Cv32F, 1);
                kernel.SetTo(values);

                // Convert the original image to a Mat object
                Mat originalMat = originalImage.Mat;

                // Apply the filter mask to the original image
                // Apply the filter mask to the original image
                Mat resultMat = new Mat(originalMat.Size, originalMat.Depth, originalMat.NumberOfChannels);

                CvInvoke.Filter2D(originalMat, resultMat, kernel, new System.Drawing.Point(-1, -1));



                // Convert the resulting Mat object to an Image<Bgr, byte> object
                Image<Bgr, byte> resultImage = resultMat.ToImage<Bgr, Byte>();

                // Convert the Image<Bgr, byte> object to a BitmapImage
                BitmapImage bitmapImage = resultImage.ToBitmap().ToBitmapImage();

                // Display the result in the WPF Image control
                imageModified.Source = bitmapImage;
              


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas przetwarzania obrazu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Konwertuj obiekt BitmapSource na obiekt Mat
     
        // Konwertuj obiekt Mat na obiekt BitmapImage
       

    }
}
