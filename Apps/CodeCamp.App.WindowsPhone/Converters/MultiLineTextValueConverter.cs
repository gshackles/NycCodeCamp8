using System;
using System.Globalization;
using System.Windows.Data;

namespace CodeCamp.App.WindowsPhone.Converters
{
    public class MultiLineTextValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return value.ToString().Replace("{nl}", Environment.NewLine);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}