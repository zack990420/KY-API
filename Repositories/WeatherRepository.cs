using KYAPI.Data;
using KYAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace KYAPI.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly AppDbContext _context;

    public WeatherRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherForecastEntity>> GetAllAsync()
    {
        return await _context.WeatherForecasts.ToListAsync();
    }

    public async Task AddAsync(WeatherForecastEntity entity)
    {
        _context.WeatherForecasts.Add(entity);
        await _context.SaveChangesAsync();
    }
}
