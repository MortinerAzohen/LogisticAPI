using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.Dtos
{
    /// <summary>
    /// RoadDto for transfering only needed data.
    /// I've added starting country and destiation country to road as it could be usefull information.
    /// </summary>
    public class RoadDto
    {
        public string StartingCountryCode { get; set; } = "USA";
        public string DestinationCountryCode { get; set; }
        public List<string> Road { get; set; }
    }
}
