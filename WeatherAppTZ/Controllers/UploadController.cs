using Microsoft.AspNetCore.Mvc;
using WeatherAppTZ.Services;

namespace WeatherAppTZ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : Controller
{
    private readonly IExcelService _excelService;

    public UploadController(IExcelService excelService)
    {
        _excelService = excelService;
    }

    [HttpPost]
    public IActionResult Upload(List<IFormFile> files)
    {
        try
        {
            _excelService.ReadExcel(files);
            return RedirectToAction("ViewArchives", "Page");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
   
}
