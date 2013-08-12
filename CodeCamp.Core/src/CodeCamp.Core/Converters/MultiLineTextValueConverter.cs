using System;
using Cirrious.CrossCore.Converters;

namespace CodeCamp.Core.Converters
{
    public class MultiLineTextValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return value.ToString().Replace("{nl}", Environment.NewLine);
        }
    }
}