using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Entities;

public class WeatherForecastEntity
{
    [Key]
    public long Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}
