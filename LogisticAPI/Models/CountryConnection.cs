using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.Models
{
    /// <summary>
    /// Using connections as it is easier to add or delete connection in the future.
    /// For example if client wants to add connection between two countries (by plane or ship)
    /// we only need to add one connection instead of changing all roads.
    /// I've added cost of road, as potentially we could add functionality that will calculate costs
    /// of roads based of connection type (by plane, truck, ship) and other parameters. Now cost of road is 
    /// always one in database. 
    /// </summary>
    public class CountryConnection
    {
        public int Id { get; set; }
        public int CountryAId { get; set; }
        public int CountryBId { get; set; }
        public int CostOfRoad { get; set; }
    }
}
