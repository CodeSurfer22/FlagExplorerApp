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

    }

}