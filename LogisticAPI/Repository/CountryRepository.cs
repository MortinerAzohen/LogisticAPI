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
        public async Task<List<Country>> GetCountries()
        {
            var counrtyList = await _db.Countries.ToListAsync();
            return counrtyList;
        }

        public async Task<Country> GetCountryByCode(string code)
        {
            var country = await _db.Countries.FirstOrDefaultAsync(c => c.CountryCode == code);
            return country;
        }

        public async Task<List<string>> GetRoadFromUSA(int startingCountryId, int destinationCountryID)
        {
            var listOfCountryCodes = new List<string>();
            var countedCountries = _db.Countries.Count();
            var matrix = CreateMatrix(countedCountries);
            var shortestPath = SearchingAlgorithms.DijkstraShortestPathAlgorithm(matrix, startingCountryId - 1, destinationCountryID - 1);

            foreach(var countryID in shortestPath)
            {
                var country = await _db.Countries.FirstOrDefaultAsync(c => c.Id == countryID + 1);
                listOfCountryCodes.Add(country.CountryCode);
            }

            return listOfCountryCodes;
        }


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
