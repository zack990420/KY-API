using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KYAPI.DTOs;
using KYAPI.Entities;
using KYAPI.Repositories;

namespace KYAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherRepository _repository;
    private readonly KYAPI.Services.IIdHasher _idHasher;

    public WeatherForecastController(IWeatherRepository repository, KYAPI.Services.IIdHasher idHasher)
    {
        _repository = repository;
        _idHasher = idHasher;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecastDto>> Get()
    {
        var entities = await _repository.GetAllAsync();
        
        // If empty, seed some data (just for demo purposes to match original behavior)
        if (!entities.Any())
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var rng = new Random();
            for (int i = 1; i <= 5; i++)
            {
                var entity = new WeatherForecastEntity
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = summaries[rng.Next(summaries.Length)]
                };
                await _repository.AddAsync(entity);
            }
            entities = await _repository.GetAllAsync();
        }

        return entities.Select(e => new WeatherForecastDto(_idHasher.Hash(e.Id), e.Date, e.TemperatureC, e.Summary));
    }
}
