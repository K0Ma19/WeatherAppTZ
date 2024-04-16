using Microsoft.EntityFrameworkCore;
using Weather.DataAccess.Models;

namespace Weather.DataAccess;

public class ApplicationContext : DbContext
{
    public DbSet<WeatherData> WeatherData { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        : base(options)
    {
        Database.EnsureCreated();
    } 
}
