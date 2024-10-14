using System.Net.Mime;
using MassTransit;
using Web.Contract;
using Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapHub<WeatherHub>("/weatherHub");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


