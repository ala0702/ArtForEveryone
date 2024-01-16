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
    /// Interaction logic for ColorPickerWindow.xaml
    /// </summary>
    public partial class ColorPickerWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        public ColorPickerWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            this.DataContext = _mainWindowViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte red = Convert.ToByte(txtRed.Text);
                byte green = Convert.ToByte(txtGreen.Text);
                byte blue = Convert.ToByte(txtBlue.Text);

                if (IsValidColorValue(red) && IsValidColorValue(green) && IsValidColorValue(blue))
                { 
                    _mainWindowViewModel.Red = red;
                    _mainWindowViewModel.Green = green;
                    _mainWindowViewModel.Blue = blue;
                }
                else
                {
                    // Komunikat o błędzie - wartości kolorów poza zakresem
                    MessageBox.Show("Wartości kolorów muszą być w zakresie od 0 do 255.");
                }
            }
            catch (FormatException)
            {
                // Komunikat o błędzie - nieprawidłowy format liczby
                MessageBox.Show("Wprowadzono nieprawidłowy format liczby.");
            }
            catch (OverflowException)
            {
                // Komunikat o błędzie - przekroczenie zakresu liczby
                MessageBox.Show("Przekroczono zakres liczby.");
            }
        }

        private bool IsValidColorValue(byte value)
        {
            return value >= 0 && value <= 255;
        }
    }
}
