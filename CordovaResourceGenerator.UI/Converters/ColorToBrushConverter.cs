using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Converter that converts a color (Windows Forms) into a Brush (WPF).
    /// </summary>
    public class ColorToBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts the color into a brush.
        /// </summary>
        /// <param name="value">The color.</param>
        /// <param name="targetType">The brush type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture of the convertion.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Drawing.Color color)
                return new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            else
                return null;
        }

        /// <summary>
        /// Converts the brush into a color. 
        /// Not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}