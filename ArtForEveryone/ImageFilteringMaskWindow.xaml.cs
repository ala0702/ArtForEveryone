using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ArtForEveryone
{
    /// <summary>
    /// Interaction logic for ImageFilteringMaskWindow.xaml
    /// </summary>
    public partial class ImageFilteringMaskWindow : Window
    {
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
                image.Source = new BitmapImage(new Uri(openDialog.FileName));

            }

        }
    }
}
