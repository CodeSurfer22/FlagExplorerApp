using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FlagExplorer.Shared.Models;

namespace FlagExplorer.API.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountryService> _logger;

        public CountryService(HttpClient httpClient, ILogger<CountryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            try
            {
                var countries = await _httpClient.GetFromJsonAsync<List<Country>>("https://restcountries.com/v3.1/all");
                return countries ?? new List<Country>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all countries.");
                throw;
            }
        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            try
            {
                var details = await _httpClient.GetFromJsonAsync<List<Country>>($"https://restcountries.com/v3.1/name/{name}");
                return details?.FirstOrDefault() ?? new Country();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching country by name.");
                throw;
            }
        }
    }
}
