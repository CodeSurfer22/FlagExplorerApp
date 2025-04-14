namespace FlagExplorer.BlazorApp.Tests
{
    using Bunit;
    using Xunit;
    using FlagExplorer.Client.Pages;
    using FlagExplorer.Shared.Models;
    using System.Net.Http;
    using System.Net;
    using System.Text.Json;
    using RichardSzalay.MockHttp;
    using Microsoft.Extensions.DependencyInjection;

    public class IndexPageTests : TestContext
    {
        [Fact]
        public void Index_LoadsAndDisplaysCountries()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var countries = new Country
            {
                Name = new NameData { Common = "France" }, // Ensure Name is properly initialized
                Flags = new FlagsData { Png = "flag.png" }, // Set flags as well
                Capital = new List<string> { "Test City" }, // Example capital
                Population = 1000000 // Example population
            };
            var json = JsonSerializer.Serialize(countries);

            mockHttp.When("http://localhost:5070/api/countries")
                    .Respond("application/json", json);

            Services.AddSingleton(new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("http://localhost:5070")
            });

            // Act
            var cut = RenderComponent<Index>();

            // Assert
            cut.Markup.Contains("France");
            cut.Markup.Contains("https://example.com/france.png");
        }
    }

}