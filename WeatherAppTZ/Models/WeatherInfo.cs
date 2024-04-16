namespace WeatherAppTZ.Models;

public class WeatherInfo
{
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public double Temperature { get; set; } 
    public short? RelativeHumidity {  get; set; }
    public double DewPoint { get; set; }
    public short? Pressure { get; set; }
    public string? DirectionWind { get; set; }
    public short? WindSpeed { get; set; }
    public short? Cloudiness { get; set; }
    public short? H { get; set; }
    public short? VV { get; set; }
    public string? Weather { get; set; }
}
