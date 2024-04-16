using Weather.DataAccess.Models;

namespace WeatherAppTZ.Models.ViewModels;

public class WeatherViewModel
{
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public Month MonthSelect { get; set; }
    public IEnumerable<int>? AllYears { get; set; }
	public IEnumerable<WeatherData>? WeatherDatas { get; set; }
}

public enum Month
{
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}