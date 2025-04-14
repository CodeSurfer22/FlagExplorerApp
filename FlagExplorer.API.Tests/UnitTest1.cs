using Xunit;
using FlagExplorer.API.Controllers;
using Moq;
using FlagExplorer.API.Services;
using FlagExplorer.Shared;
using Microsoft.AspNetCore.Mvc;
using FlagExplorer.API.Controllers;
using FlagExplorer.API.Services;
using FlagExplorer.Shared.Models;
namespace FlagExplorer.API.Tests
{

    public class CountriesControllerTests
    {
        [Fact]
        public async Task Get_ReturnsCountries()
        {
            // Arrange
            var mockService = new Mock<ICountryService>();

            // Create a mock country with NameData correctly initialized
            var country = new Country
            {
                Name = new NameData { Common = "Test" }, // Ensure Name is properly initialized
                Flags = new FlagsData { Png = "flag.png" }, // Set flags as well
                Capital = new List<string> { "Test City" }, // Example capital
                Population = 1000000 // Example population
            };

            // Mock the service to return this list of countries
            mockService.Setup(s => s.GetAllCountriesAsync())
                .ReturnsAsync(new List<Country> { country });

            // Act
            var controller = new CountriesController(mockService.Object);
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var countries = Assert.IsAssignableFrom<List<Country>>(okResult.Value);

            // Verify the country count and data
            Assert.Single(countries);
            var returnedCountry = countries.First();

            // Assert that the returned country has the correct values
            Assert.Equal("Test", returnedCountry.Name.Common);
            Assert.Equal("flag.png", returnedCountry.Flags.Png);
            Assert.Equal("Test City", returnedCountry.CapitalCity);
            Assert.Equal(1000000, returnedCountry.Population);
        }

        [Fact]
        public async Task Get_ReturnsEmptyList_WhenNoCountries()
        {
            var mockService = new Mock<ICountryService>();
            mockService.Setup(s => s.GetAllCountriesAsync())
                       .ReturnsAsync(new List<Country>());

            var controller = new CountriesController(mockService.Object);
            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var countries = Assert.IsType<List<Country>>(okResult.Value);
            Assert.Empty(countries);
        }

        [Fact]
        public async Task Get_ByName_ReturnsCountry_WhenFound()
        {
            var mockService = new Mock<ICountryService>();
            var expectedCountry = new Country
            {
                Name = new NameData { Common = "France" },
                Flags = new FlagsData { Png = "france.png" },
                Capital = new List<string> { "Paris" },
                Population = 67000000
            };

            mockService.Setup(s => s.GetCountryByNameAsync("France"))
                       .ReturnsAsync(expectedCountry);

            var controller = new CountriesController(mockService.Object);
            var result = await controller.Get("France");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var country = Assert.IsType<Country>(okResult.Value);
            Assert.Equal("France", country.Name.Common);
        }

        [Fact]
        public async Task Get_ByName_ReturnsNotFound_WhenCountryIsNull()
        {
            var mockService = new Mock<ICountryService>();
            mockService.Setup(s => s.GetCountryByNameAsync("Atlantis"))
                       .ReturnsAsync((Country?)null);

            var controller = new CountriesController(mockService.Object);
            var result = await controller.Get("Atlantis");

            Assert.IsType<NotFoundResult>(result.Result);
        }


    }

}