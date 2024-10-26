using MassTransit;
using Microsoft.OpenApi.Models;
using Web.Configurations;
using Web.Hubs;
using Web.Infrustructure;
using Web.Infrustructure.Repositories;
using Web.Services.Contracts;
using Web.Services.Interfaces;
using Web.Services.Services;

var builder = WebApplication.CreateBuilder(args);


builder.AddMassTransit();

builder.Services.AddTransient<IWeatherDataRepository, WeatherDataRepository>();
builder.Services.AddTransient<IWeatherDataService, WeatherDataService>();
builder.Services.AddDbContext<WeatherDbContext>();

//Добавляем swagger
builder.Services.AddSwaggerConfig();

//Добавляем контроллеры
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddCors();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1"));

app.MapHub<WeatherHub>("/weatherHub");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


