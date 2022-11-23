using Catalog.API.Data;
using Catalog.API.Repositories;

namespace Catalog.API.Extensions
{
    public static class CatalogServiceRegistration
    {
        public static IServiceCollection AddCatalogServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
