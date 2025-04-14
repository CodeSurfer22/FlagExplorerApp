namespace FlagExplorer.BlazorApp.Tests
{
    using Bunit;
    using Xunit;
    using FlagExplorer.Client.Pages;
    using FlagExplorer.Shared.Models;
    using Microsoft.Extensions.DependencyInjection;
    using RichardSzalay.MockHttp;
    using System.Text.Json;

    public class CountryDetailPageTests : TestContext
    {
        [Fact]
        public void CountryPage_LoadsAndDisplaysCountryDetails()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            var france = new Country
            {
                Name = new NameData { Common = "France" }, // Ensure Name is properly initialized
                Flags = new FlagsData { Png = "flag.png" }, // Set flags as well
                Capital = new List<string> { "Paris" }, // Example capital
                Population = 67000000 // Example population
            };

            var json = JsonSerializer.Serialize(france);

            mockHttp.When("http://localhost:5070/api/countries/France")
                    .Respond("application/json", json);

            Services.AddSingleton(new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("http://localhost:5070")
            });

            // Act
            var cut = RenderComponent<CountryDetails>(parameters => parameters.Add(p => p.Name, "France"));

            // Assert
            cut.Markup.Contains("France");
            cut.Markup.Contains("Paris");
            cut.Markup.Contains("67000000");
        }
    }
}
