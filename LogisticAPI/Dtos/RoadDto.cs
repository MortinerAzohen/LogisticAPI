using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.Dtos
{
    public class RoadDto
    {
        public string StartingCountryCode { get; set; } = "USA";
        public string DestinationCountryCode { get; set; }
        public List<string> Road { get; set; }
    }
}
