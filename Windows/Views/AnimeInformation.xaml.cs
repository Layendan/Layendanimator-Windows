using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Windows.Views
{
    /// <summary>
    /// Interaction logic for AnimeInformation.xaml
    /// </summary>
    public partial class AnimeInformation : Page
    {
        public AnimeInformation()
        {
            InitializeComponent();
            rectangle.Fill = new SolidColorBrush(GetAverageColor((BitmapSource)image.Source));
        }

        public void setSource(ImageSource source)
        {
            image.Source = source;
            rectangle.Fill = new SolidColorBrush(GetAverageColor((BitmapSource)image.Source));
        }

        public System.Windows.Media.Color GetAverageColor(BitmapSource bitmap)
        {
            var format = bitmap.Format;

            if (format != PixelFormats.Bgr24 &&
                format != PixelFormats.Bgr32 &&
                format != PixelFormats.Bgra32 &&
                format != PixelFormats.Pbgra32)
            {
                throw new InvalidOperationException("BitmapSource must have Bgr24, Bgr32, Bgra32 or Pbgra32 format");
            }

            var width = bitmap.PixelWidth;
            var height = bitmap.PixelHeight;
            var numPixels = width * height;
            var bytesPerPixel = format.BitsPerPixel / 8;
            var pixelBuffer = new byte[numPixels * bytesPerPixel];

            bitmap.CopyPixels(pixelBuffer, width * bytesPerPixel, 0);

            long blue = 0;
            long green = 0;
            long red = 0;

            for (int i = 0; i < pixelBuffer.Length; i += bytesPerPixel)
            {
                blue += pixelBuffer[i];
                green += pixelBuffer[i + 1];
                red += pixelBuffer[i + 2];
            }

            return System.Windows.Media.Color.FromRgb((byte)(red / numPixels), (byte)(green / numPixels), (byte)(blue / numPixels));
        }
    }
}
