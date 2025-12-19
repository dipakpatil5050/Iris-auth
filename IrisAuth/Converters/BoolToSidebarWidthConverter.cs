//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IrisAuth.Converters
//{
//    internal class BoolToSidebarWidthConverter
//    {
//    }
//}
using System;
using System.Globalization;
using System.Windows.Data;

namespace IrisAuth.Converters
{
    public class BoolToSidebarWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool isExpanded = (bool)value;
                return isExpanded ? 250 : 70;
            }

            return 250;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}



