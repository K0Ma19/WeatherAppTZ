using Microsoft.AspNetCore.Mvc;
using Weather.DataAccess;
using WeatherAppTZ.Services;
using WeatherAppTZ.Models.ViewModels;

namespace WeatherAppTZ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ViewController : Controller
{
    private readonly ApplicationContext _context;
    private readonly IWeatherServices _weatherServices;
    public ViewController(ApplicationContext context, IWeatherServices weatherServices)
    {
        _context = context;
        _weatherServices = weatherServices;
    }

    [HttpGet]
    public IActionResult GetData(int? year, int? month, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var allWeatherData = _weatherServices.GetWeathersAsync(month, year).Result;
            
            var allYear = _context.WeatherData.Select(w => w.Date.Year).Distinct().ToList();
            var totalItems = allWeatherData.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var weatherData = allWeatherData.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new WeatherViewModel
            {
                Year = year,
                Month = month,
                TotalPages = totalPages,
                WeatherDatas = weatherData,
                AllYears = allYear,
            };

            return View("~/Views/Page/ViewArchives.cshtml", viewModel);
        }
        catch (Exception ex)
        {

            return BadRequest(ex);
        }
    }
}
