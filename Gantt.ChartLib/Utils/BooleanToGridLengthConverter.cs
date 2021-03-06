using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Gantt.ChartLib.Utils
{
    public class BooleanToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is true)
                return GridLength.Auto;
            else
                return new GridLength(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
