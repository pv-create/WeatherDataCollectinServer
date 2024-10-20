using Microsoft.EntityFrameworkCore;
using Web.Services.Models;

namespace Web.Infrustructure;

public class WeatherDbContext: DbContext
{
    public DbSet<WeatherData> WeatherDatas { get; set; }

    public string DbPath { get; }

    public WeatherDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "WeatherDataDb.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}", b => b.MigrationsAssembly("Web.Api"));
}