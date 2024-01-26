using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArtForEveryone
{
    public class MainWindowViewModel : ObservableObject
    {
        private byte _Red = 200;
        public byte Red
        {
            get { return _Red; }
            set
            {
                if (_Red != value)
                {
                    _Red = value;
                    OnPropertyChanged(nameof(Red));
                    UpdateHSVValues();
                    UpdateColor();
                }
            }
        }

        private byte _Green = 200;
        public byte Green
        {
            get { return _Green; }
            set
            {
                if (_Green != value)
                {
                    _Green = value;
                    OnPropertyChanged(nameof(Green));
                    UpdateHSVValues();
                    UpdateColor();
                }
            }
        }

        private byte _Blue = 200;
        public byte Blue
        {
            get { return _Blue; }
            set
            {
                if (_Blue != value)
                {
                    _Blue = value;
                    OnPropertyChanged(nameof(Blue));
                    UpdateHSVValues();
                    UpdateColor();
                }
            }
        }

        private Color _MainColor = Color.FromRgb(200, 200, 200);
        public Color MainColor
        {
            get { return _MainColor; }
            set
            {
                if (_MainColor != value)
                {
                    _MainColor = value;
                    OnPropertyChanged(nameof(MainColor));
                    //UpdateRgbValues();
                }
            }
        }

        public SolidColorBrush MainBrush => new SolidColorBrush(MainColor);


        private double _H = 0;
        public double H
        { 
            get { return _H; } 
            set
            { 
                if(_H != value)
                {
                    _H = value;
                    OnPropertyChanged(nameof(H));
                }
            }
                
        }
        private double _S = 0;
        public double S
        {
            get { return _S; }
            set
            {
                if(_S != value)
                {
                    _S = value;
                    OnPropertyChanged(nameof(S));
                }
            }
        }
        private double _V = 0;
        public double V
        {
            get { return _V; }
            set
            {
                if(_V != value)
                {
                    _V = value;
                    OnPropertyChanged(nameof(V));
                }
            }
        }

        private string _HSVFormatted;
        public string HSVFormatted
        {
            get { return _HSVFormatted; }
            set
            {
                if (_HSVFormatted != value)
                {
                    _HSVFormatted = value;
                    OnPropertyChanged(nameof(HSVFormatted));
                }
            }
        }

        private void UpdateColor()
        {
            MainColor = Color.FromRgb(Red, Green, Blue);
            OnPropertyChanged(nameof(MainBrush));
        }

        private void UpdateHSVValues()
        {
            double normalizedR = Red / 255.0;
            double normalizedG = Green / 255.0;
            double normalizedB = Blue / 255.0;

            double maxColor = Math.Max(normalizedR, Math.Max(normalizedG, normalizedB));
            double minColor = Math.Min(normalizedR, Math.Min(normalizedG, normalizedB));

            V = maxColor;
            S = (maxColor != 0) ? (maxColor - minColor) / maxColor : 0;

            
            if (maxColor == minColor)
            {
                H = 0;
            }
            else
            {
                if (maxColor == normalizedR)
                {
                    H = (60 * ((normalizedG - normalizedB) / (maxColor - minColor)) + 360) % 360;
                }
                else if (maxColor == normalizedG)
                {
                    H = (60 * ((normalizedB - normalizedR) / (maxColor - minColor)) + 120) % 360;
                }
                else
                {
                    H = (60 * ((normalizedR - normalizedG) / (maxColor - minColor)) + 240) % 360;
                }
            }

            // Formatowanie wartości HSV
            HSVFormatted = $"({Math.Round(H)}°, {Math.Round(S * 100)}%, {Math.Round(V * 100)}%)";

        }
        private void UpdateRgbValues()
        {
            Red = MainColor.R;
            Green = MainColor.G;
            Blue = MainColor.B;
            OnPropertyChanged(nameof(Red));
            OnPropertyChanged(nameof(Green));
            OnPropertyChanged(nameof(Blue));
        }
    }
}
