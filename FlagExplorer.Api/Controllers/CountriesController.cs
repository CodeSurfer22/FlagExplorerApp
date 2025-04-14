using Microsoft.AspNetCore.Mvc;
using FlagExplorer.API.Services;
using FlagExplorer.Shared;
using FlagExplorer.Shared.Models;

namespace FlagExplorer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Country>>> Get()
    {
        var countries = await _countryService.GetAllCountriesAsync();
        return Ok(countries);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Country>> Get(string name)
    {
        var country = await _countryService.GetCountryByNameAsync(name);
        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }
}
