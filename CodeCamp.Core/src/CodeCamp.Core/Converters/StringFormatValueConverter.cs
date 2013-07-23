using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace CodeCamp.Core
{
    public class StringFormatValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (parameter == null)
                return value;

            return string.Format("{0:" + parameter + "}", value);
        }
    }
}