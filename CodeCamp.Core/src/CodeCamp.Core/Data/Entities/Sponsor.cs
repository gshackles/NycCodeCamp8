using System.Collections.Generic;

namespace CodeCamp.Core.Data.Entities
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string Tier { get; set; }
    }
}