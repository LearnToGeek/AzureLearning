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
    }
}
