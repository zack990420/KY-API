using System.ComponentModel.DataAnnotations;

namespace KYAPI.Entities;

public class WeatherForecastEntity
{
    [Key]
    public long Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}
