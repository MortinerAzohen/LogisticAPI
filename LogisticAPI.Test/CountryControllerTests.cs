using LogisticAPI.Controllers;
using LogisticAPI.Dtos;
using LogisticAPI.Models;
using LogisticAPI.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LogisticAPI.Test
{
    public class CountryControllerTests
    {
        private readonly Mock<ICountryRepository> _mockRepo;
        private readonly CountryController _countryController;

        public CountryControllerTests()
        {
            _mockRepo = new Mock<ICountryRepository>();
            _countryController = new CountryController(_mockRepo.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsResponse()
        {
            var result = _countryController.Get();

            Assert.IsType<Task<ResponseDto>>(result);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsCExactNumberOfCountries()
        {
            _mockRepo.Setup(repo => repo.GetCountries().Result)
                                        .Returns(new List<Country>() { new Country(), new Country() });
            var result = _countryController.Get().Result;

            List<Country> c = (List<Country>)result.Result;

            Assert.Equal(2, c.Count);
        }
        [Fact]
        public void GetShortestRoadFromUSA_WhenCalled_ReturnsResponse()
        {
            var result = _countryController.GetShortestRoadFromUSA(It.IsAny<string>());

            Assert.IsType<Task<ResponseDto>>(result);
        }
        [Fact]
        public void GetShortestRoadFromUSA_WhenCalled_ReturnsResponseWithRoadDto()
        {
            //arragne
            var USA = new Country
            {
                Id = 1,
                CountryCode = "USA",
                CountryName = "United States"
            };
            _mockRepo.Setup(repo => repo.GetCountryByCode("USA").Result)
                     .Returns(USA);
            _mockRepo.Setup(repo => repo.GetCountryByCode(It.IsAny<string>()).Result)
                     .Returns(new Country());
            _mockRepo.Setup(repo => repo.GetRoadFromUSA(USA.Id, It.IsAny<int>()).Result)
                     .Returns(new List<string>());

            //act
            var result = _countryController.GetShortestRoadFromUSA(It.IsAny<string>()).Result;

            //assert
            Assert.IsType<ResponseDto>(result);
            Assert.IsType<RoadDto>(result.Result);
            Assert.True(result.IsSuccess == true);
        }
        [Fact]
        public void GetShortestRoadFromUSA_WhenCalled_ReturnsResponseWithCorrectRoad()
        {
            //arragne
            var USA = new Country
            {
                Id = 1,
                CountryCode = "USA",
                CountryName = "United States"
            };
            var otherCountry = new Country
            {
                Id = 2,
                CountryCode = "HND",
                CountryName = "Honduras"
            };
            var road = new List<string>() { "USA", "MEX", "GTM", "HND" };

            _mockRepo.Setup(repo => repo.GetCountryByCode("USA").Result)
                     .Returns(USA);
            _mockRepo.Setup(repo => repo.GetCountryByCode("HND").Result)
                     .Returns(otherCountry);
            _mockRepo.Setup(repo => repo.GetRoadFromUSA(USA.Id, otherCountry.Id).Result)
                     .Returns(road);

            //act
            var result = _countryController.GetShortestRoadFromUSA("HND").Result;
            RoadDto list = (RoadDto)result.Result;

            //assert
            Assert.IsType<ResponseDto>(result);
            Assert.IsType<RoadDto>(result.Result);
            Assert.True(result.IsSuccess == true);
            Assert.Equal(road, list.Road);
        }
        [Fact]
        public void GetShortestRoadFromUSA_WhenCalledWithUsaCountryCode_ReturnsResponseWithCorrectErrorMsg()
        {
            //arragne
            var countryCode = "USA";
            var USA = new Country
            {
                Id = 1,
                CountryCode = "USA",
                CountryName = "United States"
            };
            var errorMsg = $"Starting country is same as destination country. Please choose other country than {countryCode}";

            _mockRepo.Setup(repo => repo.GetCountryByCode(countryCode).Result)
                     .Returns(USA);

            //act
            var result = _countryController.GetShortestRoadFromUSA(countryCode).Result;

            //assert
            Assert.True(result.IsSuccess == false);
            Assert.Equal(errorMsg, result.ErrorMsg);
        }
        [Fact]
        public void GetShortestRoadFromUSA_WhenCalledWithNotExistingCountryCode_ReturnsResponseWithCorrectErrorMsg()
        {
            //arragne
            var countryCode = It.IsAny<string>();
            var errorMsg = $"Country with given code {countryCode} doesn't exist";

            //act
            var result = _countryController.GetShortestRoadFromUSA(countryCode).Result;

            //assert
            Assert.True(result.IsSuccess == false);
            Assert.Equal(errorMsg, result.ErrorMsg);
        }
    }
}
