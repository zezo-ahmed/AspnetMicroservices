using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

namespace OcelotApiGw.Extensions
{
    public static class ApiGwServiceRegistration
    {
        public static IServiceCollection AddApiGwServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddOcelot() // Dependency Injection
                    .AddCacheManager(config => config.WithDictionaryHandle()); // cache Response

            builder.Host.ConfigureAppConfiguration((ctx, cfg) =>
            {
                Console.WriteLine($"Environment Name: {ctx.HostingEnvironment.EnvironmentName}");
                cfg.AddJsonFile($"ocelot.{ctx.HostingEnvironment.EnvironmentName}.json", true, true);
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            });

            return services;
        }
    }
}
