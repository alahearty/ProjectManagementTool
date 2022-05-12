using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Gantt.ChartLib.Utils
{
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return new BrushConverter().ConvertFromString(value.ToString());
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString();
            }
            return null;
        }
    }
}
