using LogisticAPI.Dtos;
using LogisticAPI.Repository;
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
        [HttpGet]
        [Route("api/countries")]
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
        [HttpGet]
        [Route("{countryCode}")]
        public async Task<ResponseDto> GetShortestRoadFromUSA(string countryCode)
        {
            var destinationcountry = await _countryRepository.GetCountryByCode(countryCode);
            
            if(destinationcountry == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMsg = $"Country with given code {countryCode} doesn't exist";
                return _response;
            }
            else if(destinationcountry.CountryCode == "USA")
            {
                _response.IsSuccess = false;
                _response.ErrorMsg = $"Starting country is same as destination country. Please choose other country than {destinationcountry}";
                return _response;
            }
            
            try
            {

                var road = new RoadDto()
                {
                    StartingCountryCode = "USA",
                    DestinationCountryCode = destinationcountry.CountryCode,
                    Road = await _countryRepository.GetRoadFromUSA(2, destinationcountry.Id)
                };
                _response.Result = road;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMsg = ex.ToString();
            }
            return _response;
        }
    }
}
