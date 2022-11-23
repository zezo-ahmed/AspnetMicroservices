using MassTransit;
using System.Reflection;
using EventBus.Messages.Common;
using Ordering.API.EventBusConsumer;

namespace Ordering.API.Extensions
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // General Configuration
            services.AddScoped<BasketCheckoutConsumer>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<BasketCheckoutConsumer>(); // <<=====

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>  // <<=====
                    {
                        c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    });
                });
            });

            //services.AddOptions<MassTransitHostOptions>().Configure(options =>
            //{
            //    options.WaitUntilStarted = true;

            //    options.StartTimeout = TimeSpan.FromSeconds(10);

            //    options.StopTimeout = TimeSpan.FromSeconds(30);
            //});

            return services;
        }
    }
}
