using MassTransit;
using System.Reflection;
using Discount.Grpc.Protos;
using Basket.API.GrpcServices;
using Basket.API.Repositories;

namespace Basket.API.Extensions
{
    public static class BasketServiceRegistration
    {
        public static IServiceCollection AddBasketServices(this IServiceCollection services, IConfiguration configuration)
        {
            // General Configuration
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IBasketRepository, BasketRepository>();

            // Grpc Configuration
            services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
            {
                opt.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]);
            });

            // Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
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
