using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCamp.Core
{
    public class Camp
    {
        public int CampId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime EventDate { get; set; }
        public int Length { get; set; }
        public Location Location { get; set; }
        public ICollection<Talk> Talks { get; set; }

    }

    
}
