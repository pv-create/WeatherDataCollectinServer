using MassTransit;
using Microsoft.OpenApi.Models;
using Web.Hubs;
using Web.Infrustructure;
using Web.Infrustructure.Repositories;
using Web.Services.Contracts;
using Web.Services.Interfaces;
using Web.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<RawWeatherDataConsumer>();

    busConfigurator.UsingRabbitMq((context, configurtor) =>
    {
        configurtor.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]!);
            h.Password(builder.Configuration["MessageBroker:Password"]!);
        });
        
        configurtor.ReceiveEndpoint("weather_data", e =>
        {
            e.Durable = false;
            e.ClearSerialization();
            e.UseRawJsonSerializer();
            e.ConfigureConsumer<RawWeatherDataConsumer>(context);
        });
    });
});

builder.Services.AddSignalR();

builder.Services.AddTransient<IWeatherDataRepository, WeatherDataRepository>();
builder.Services.AddTransient<IWeatherDataService, WeatherDataService>();
builder.Services.AddDbContext<WeatherDbContext>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
});

builder.Services.AddControllersWithViews();

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
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


