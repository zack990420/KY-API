using KYAPI.Entities;

namespace KYAPI.Repositories;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecastEntity>> GetAllAsync();
    Task AddAsync(WeatherForecastEntity entity);
}
