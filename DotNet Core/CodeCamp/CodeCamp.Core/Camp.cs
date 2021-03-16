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

    }

    public class Location
    {
        public int LocationId { get; set; }
        public string VenueName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityTown { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
