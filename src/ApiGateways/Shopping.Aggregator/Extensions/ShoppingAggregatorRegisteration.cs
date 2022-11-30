using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Extensions
{
    public static class ShoppingAggregatorRegisteration
    {
        public static IServiceCollection AddShoppingAggregatorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICatalogService, CatalogService>(client =>
            {
                client.BaseAddress = new Uri(configuration["ApiSettings:CatalogUrl"]);
            });

            services.AddHttpClient<IBasketService, BasketService>(client =>
            {
                client.BaseAddress = new Uri(configuration["ApiSettings:BasketUrl"]);
            });

            services.AddHttpClient<IOrderService, OrderService>(client =>
            {
                client.BaseAddress = new Uri(configuration["ApiSettings:OrderingUrl"]);
            });

            return services;
        }
    }
}
