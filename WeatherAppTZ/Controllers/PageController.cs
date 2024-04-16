using Microsoft.AspNetCore.Mvc;

namespace WeatherAppTZ.Controllers;

public class PageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult ViewArchives()
    {
        return View();
    }
    public IActionResult UploadArchives()
    {
        return View();
    }
}
