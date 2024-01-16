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

        private void UpdateColor()
        {
            MainColor = Color.FromRgb(Red, Green, Blue);
            OnPropertyChanged(nameof(MainBrush));
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
