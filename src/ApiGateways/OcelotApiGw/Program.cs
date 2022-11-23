using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOcelot() // Dependency Injection
    .AddCacheManager(config => config.WithDictionaryHandle()); // cache Response

builder.Host
       .ConfigureAppConfiguration((ctx, cfg) =>
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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();
