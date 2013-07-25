using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using CodeCamp.Core.Extensions;

namespace CodeCamp.Core
{
    public class TimeValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(DateTime))
                return null;

            return ((DateTime)value).FormatTime();
        }
    }
}