using System;
using Cirrious.CrossCore.Converters;
using CodeCamp.Core.Data.Entities;

namespace CodeCamp.Core.Converters
{
    public class SessionDetailsConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(Session))
                return null;

            var session = (Session)value;

            return string.Format("{0}, {1}", session.RoomName, session.SpeakerName);
        }
    }
}