using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCamp.Core.Models
{
    public class CampModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime EventDate { get; set; }
        public int Length { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public ICollection<TalkModel> Talks { get; set; }
    }
}
