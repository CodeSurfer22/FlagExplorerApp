using System.Net;
using System.Net.Http.Json;
using FlagExplorer.API;
using FlagExplorer.Shared.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace FlagExplorer.API.IntegrationTests
{
    public class CountriesApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CountriesApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(); // This launches the API in-memory for testing
        }

        [Fact]
        public async Task GetAllCountries_ReturnsSuccessAndList()
        {
            // Act
            var response = await _client.GetAsync("/api/countries");

            // Assert
            response.EnsureSuccessStatusCode(); // Status 200-299
            var countries = await response.Content.ReadFromJsonAsync<List<Country>>();

            Assert.NotNull(countries);
            Assert.NotEmpty(countries);
        }

        [Theory]
        [InlineData("germany")]
        [InlineData("thailand")]
        public async Task GetCountryByName_ReturnsSuccess_WhenFound(string countryName)
        {
            // Act
            var response = await _client.GetAsync($"/api/countries/{countryName}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status 200 OK
            var country = await response.Content.ReadFromJsonAsync<Country>();

            Assert.NotNull(country);
            Assert.False(string.IsNullOrWhiteSpace(country.Name?.Common));
        }
    }
}
