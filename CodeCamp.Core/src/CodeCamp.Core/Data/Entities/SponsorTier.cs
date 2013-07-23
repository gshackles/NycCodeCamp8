using System.Collections.Generic;

namespace CodeCamp.Core.Data.Entities
{
    public class SponsorTier
    {
        public string Name { get; set; }
        public IList<Sponsor> Sponsors { get; set; }
    }
}