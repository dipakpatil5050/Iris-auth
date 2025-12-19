using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IrisAuth.Converters
{
    public class BoolToStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool connected = value is bool && (bool)value;
            return connected ? Brushes.Green : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
