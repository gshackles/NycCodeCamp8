using System.Collections.Generic;

namespace CodeCamp.Core.Data.Entities
{
    public class CampData
    {
        public IList<Session> Sessions { get; set; }
        public IList<Speaker> Speakers { get; set; }
        public IList<Sponsor> Sponsors { get; set; }
    }
}
