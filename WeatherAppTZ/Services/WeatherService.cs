using Microsoft.EntityFrameworkCore;
using Weather.DataAccess.Models;
using WeatherAppTZ.Models;
using Weather.DataAccess;

namespace WeatherAppTZ.Services;

public interface IWeatherServices
{
    Task CreateWeatherAsync(IEnumerable<WeatherInfo> weatherDatas);
    Task<IEnumerable<WeatherData>> GetWeathersAsync(int? month, int? year);
}
public class WeatherService : IWeatherServices
{
    private readonly ApplicationContext _context;

    public WeatherService(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateWeatherAsync(IEnumerable<WeatherInfo> weatherDatas)
    {
        var entities = new List<WeatherData>();
        foreach (var item in weatherDatas)
        {
            if (_context.WeatherData.Any(w => w.Date == item.Date && w.Time == item.Time))
                continue;

            entities.Add(new WeatherData
            {
                Date = item.Date,
                Cloudiness = item.Cloudiness,
                DewPoint = item.DewPoint,
                DirectionWind = item.DirectionWind,
                H = item.H,
                Pressure = item.Pressure,
                RelativeHumidity = item.RelativeHumidity,
                Temperature = item.Temperature,
                Time = item.Time,
                VV = item.VV,
                Weather = item.Weather,
                WindSpeed = item.WindSpeed
            });
        }

        await _context.WeatherData.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<WeatherData>> GetWeathersAsync(int? month, int? year)
    {
        IQueryable<WeatherData> query = _context.WeatherData;

        if (month != null)
        {
            query = query.Where(w => w.Date.Month == month);
        }

        if (year != null)
        {
            query = query.Where(w => w.Date.Year == year);
        }

        return await query.ToListAsync();
    }
}
