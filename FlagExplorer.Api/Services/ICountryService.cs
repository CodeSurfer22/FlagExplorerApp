using FlagExplorer.Shared;
using FlagExplorer.Shared.Models;

namespace FlagExplorer.API.Services;

public interface ICountryService
{
    Task<List<Country>> GetAllCountriesAsync();
    Task<Country> GetCountryByNameAsync(string name);
}
