using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FNet.FSA.WPF.Converters
{
    public class InverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == Visibility.Hidden.GetType())
            {
                if ((Visibility)value == Visibility.Visible) return Visibility.Collapsed;
                if ((Visibility)value == Visibility.Collapsed) return Visibility.Visible;
            }
            else if (value.GetType() == true.GetType())
            {
                return !(bool)value;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == Visibility.Hidden.GetType())
            {
                if ((Visibility)value == Visibility.Visible) return Visibility.Collapsed;
                if ((Visibility)value == Visibility.Collapsed) return Visibility.Visible;
            }
            else if (value.GetType() == true.GetType())
            {
                return !(bool)value;
            }

            return value;
        }
    }
}
