using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherAppTZ.Models;

namespace WeatherAppTZ.Services;

public interface IExcelService
{
    public Task ReadExcel(IEnumerable<IFormFile> file);
}
public class ExcelService : IExcelService
{
    private readonly IWeatherServices _weatherServices;
    public ExcelService(IWeatherServices weatherServices)
    {
        _weatherServices = weatherServices;
    }
    public async Task ReadExcel(IEnumerable<IFormFile> files)
    {
        var weatherData = new List<WeatherInfo>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        IWorkbook workbook;
                        using (stream)
                        {
                            workbook = new XSSFWorkbook(stream);
                        }

                        for (int i = 0; i < workbook.NumberOfSheets; i++)
                        {
                            ISheet sheet = workbook.GetSheetAt(i);

                            for (int j = 4; j <= sheet.LastRowNum; j++)
                            {
                                IRow row = sheet.GetRow(j);
                                if (row != null)
                                {
                                    WeatherInfo temp = new();
                                    var date = row.GetCell(0).AsDateOnlyOrNull();
                                    var time = row.GetCell(1).AsTimeOnlyOrNull();

                                    temp.Date = date;
                                    temp.Time = time;
                                    temp.Temperature = row.GetCell(2).NumericCellValue;
                                    temp.RelativeHumidity = row.GetCell(3).AsShortOrNull();
                                    temp.DewPoint = row.GetCell(4).NumericCellValue;
                                    temp.Pressure = row.GetCell(5).AsShortOrNull();
                                    temp.DirectionWind = row.GetCell(6).StringCellValue;
                                    temp.WindSpeed = row.GetCell(7).AsShortOrNull();
                                    temp.Cloudiness = row.GetCell(8).AsShortOrNull();
                                    temp.H = row.GetCell(9).AsShortOrNull();
                                    temp.VV = row.GetCell(10).AsShortOrNull();
                                    temp.Weather = row.GetCell(11)?.ToString();

                                    weatherData.Add(temp);
                                }

                            }

                        }

                        _weatherServices.CreateWeatherAsync(weatherData).Wait();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка при обработке файла Excel: " + ex.Message);
                    }
                }
            }
        }
    }
}
public static class ExcelExtensions
{
    public static DateOnly AsDateOnlyOrNull(this ICell cell)
    {
        if (DateOnly.TryParse(cell.ToString(), out var parsedDate))
        {
            return parsedDate;
        }
        return parsedDate;
    }
    public static TimeOnly AsTimeOnlyOrNull(this ICell cell)
    {
        if (TimeOnly.TryParse(cell.ToString(), out var parsedDate))
        {
            return parsedDate;
        }
        return parsedDate;
    }

    public static short? AsShortOrNull(this ICell cell)
    {
        if (short.TryParse(cell.ToString(), out var parsedDate))
        {
            return parsedDate;
        }
        return null;
    }
}
