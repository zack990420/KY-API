using MyWebApi.Entities;

namespace MyWebApi.Repositories;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecastEntity>> GetAllAsync();
    Task AddAsync(WeatherForecastEntity entity);
}
