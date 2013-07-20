using System;
using System.Collections.Generic;

namespace CodeCamp.Core.Data.Entities
{
    public class TimeSlot
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public IList<Session> Sessions { get; set; }
    }
}