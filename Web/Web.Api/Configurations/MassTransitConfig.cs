using MassTransit;

namespace Web.Configurations;

public static class MassTransitConfig
{
    public static void AddMassTransit(this WebApplicationBuilder builder)
    {
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

    }
}