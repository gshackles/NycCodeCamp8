using System;
using System.Collections.Generic;
using CodeCamp.Core.Extensions;

namespace CodeCamp.Core.Data.Entities
{
    public class TimeSlot
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public IList<Session> Sessions { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", StartTime.FormatTime(), EndTime.FormatTime());
        }
    }
}