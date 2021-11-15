using LogisticAPI.DbContexts;
using LogisticAPI.Dtos;
using LogisticAPI.Models;
using LogisticAPI.Support;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _db;
        public CountryRepository(AppDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Basic operation to retrive all countries. I've created this function in repository
        /// as it was easier for me to test database connection on simpler function.
        /// Possibly in API it would be created anyway as it is basic functionality. 
        /// </summary>
        /// <returns>List of all countries in database</returns>
        public async Task<List<Country>> GetCountries()
        {
            var counrtyList = await _db.Countries.ToListAsync();
            return counrtyList;
        }
        /// <summary>
        /// Function returns country with specific country code. Id of country is needed to create road.
        /// I'm returning whole object not just Id because that function may be usufull in future. 
        /// For example as Country class may have more properties with additional information and client 
        /// wants to get information about country by passing only country code.
        /// </summary>
        /// <param name="code">Country code stored in database</param>
        /// <returns>Country object that has that country code.</returns>
        public async Task<Country> GetCountryByCode(string code)
        {
            var country = await _db.Countries.FirstOrDefaultAsync(c => c.CountryCode == code);
            return country;
        }
        /// <summary>
        /// Function returns road from destination A to destiantion B. In this case it will be always from but 
        /// it will return any road between north america countries. I didn't hard coded 'USA' because
        /// in my opinion there is huge possibility that client will ask for roads starting from other countries than USA.
        /// I used searching algorythm to find the best road. 
        /// </summary>
        /// <param name="startingCountryId"> starting country code</param>
        /// <param name="destinationCountryID"> destination country code</param>
        /// <returns> returns list of strings with country codes</returns>
        public async Task<List<string>> GetRoadFromUSA(int startingCountryId, int destinationCountryID)
        {
            var listOfCountryCodes = new List<string>();
            var countedCountries = _db.Countries.Count();
            //Creating matrix needed for Searching algorythm.
            var matrix = CreateMatrix(countedCountries);

            // Dijkstra algorythm needs matrix with cost values between nodes. I've found simplest implementation in the internet and 
            // changed it for puroposes of this recrutation task.
            var shortestPath = SearchingAlgorithms.DijkstraShortestPathAlgorithm(matrix, startingCountryId - 1, destinationCountryID - 1);

            foreach(var countryID in shortestPath)
            {
                var country = await _db.Countries.FirstOrDefaultAsync(c => c.Id == countryID + 1);
                listOfCountryCodes.Add(country.CountryCode);
            }

            return listOfCountryCodes;
        }

        /// <summary>
        /// This function creates matrix for algorythm. It only works for all countries from database so countedCountries
        /// always should be equal of _db.Countries.Count();
        /// 
        /// </summary>
        /// <param name="countedCountries">amout of countries that we want to add to matrix</param>
        /// <returns></returns>
        private int[,] CreateMatrix(int countedCountries)
        {
            var connections = _db.CountryConnections.ToList();

            var matrix = new int[countedCountries, countedCountries];

            foreach(var connection in connections)
            {
                matrix[connection.CountryAId - 1, connection.CountryBId - 1] = connection.CostOfRoad;
                matrix[connection.CountryBId - 1, connection.CountryAId - 1] = connection.CostOfRoad;
            }

            return matrix;
        }
    }
}
