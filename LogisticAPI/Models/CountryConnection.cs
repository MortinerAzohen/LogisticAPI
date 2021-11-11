using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.Models
{
    public class CountryConnection
    {
        public int Id { get; set; }
        public int CountryAId { get; set; }
        public int CountryBId { get; set; }
        public int CostOfRoad { get; set; }
    }
}
