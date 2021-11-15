using LogisticAPI.Dtos;
using LogisticAPI.Repository;
using LogisticAPI.Support;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.Controllers
{
    
    [ApiController]
    public class CountryController : ControllerBase
    {
        protected ResponseDto _response;
        private ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _response = new ResponseDto();
            _countryRepository = countryRepository;
        }

        /// <summary>
        /// API controller funtion  to get all countries from database
        /// </summary>
        /// <returns>ResponseDto with list of countries</returns>
        [HttpGet]
        [Route("countries")]
        public async Task<ResponseDto> Get()
        {
            try
            {
                var countries = await _countryRepository.GetCountries();
                _response.Result = countries;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMsg = ex.ToString();
            }
            return _response;
        }
        /// <summary>
        /// API controller function to get road between USA and other country
        /// </summary>
        /// <param name="countryCode">code for destination country</param>
        /// <returns>Response with RoadDto with shortest road</returns>
        [HttpGet]
        [Route("{countryCode}")]
        public async Task<ResponseDto> GetShortestRoadFromUSA(string countryCode)
        {
            // getting countries by CountryCodes. For USA I used constant value as it is easier to not make spelling mistake.
            // It's easier to change Use country code if there will be need for it.
            var startingCountry = await _countryRepository.GetCountryByCode(Constants.USACountryCode);
            var destinationcountry = await _countryRepository.GetCountryByCode(countryCode);
            
            // Checking if destination country exist if not returning proper error msg in response
            if(destinationcountry == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMsg = $"Country with given code {countryCode} doesn't exist";
                return _response;
            }
            // checking if destination is not USA and returning response with error msg
            else if(destinationcountry.CountryCode == Constants.USACountryCode)
            {
                _response.IsSuccess = false;
                _response.ErrorMsg = $"Starting country is same as destination country. Please choose other country than {destinationcountry.CountryCode }";
                return _response;
            }
            

            try
            {
            // creating RoadDto with all data
                var road = new RoadDto()
                {
                    StartingCountryCode = Constants.USACountryCode,
                    DestinationCountryCode = destinationcountry.CountryCode,
                    Road = await _countryRepository.GetRoadFromUSA(startingCountry.Id, destinationcountry.Id)
                };
                _response.Result = road;
            }
            // if there is an error response will be sended with error msg.
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMsg = ex.ToString();
            }
            return _response;
        }
    }
}
