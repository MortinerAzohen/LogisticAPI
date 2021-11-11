using LogisticAPI.Dtos;
using LogisticAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.Repository
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountries();
        Task<Country> GetCountryByCode(string code);
        Task<List<string>> GetRoadFromUSA(int startingCountryId, int destinationCountryID);
    }
}
