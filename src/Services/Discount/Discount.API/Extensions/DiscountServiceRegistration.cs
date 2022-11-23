using Discount.API.Repositories;

namespace Discount.API.Extensions
{
    public static class DiscountServiceRegistration
    {
        public static IServiceCollection AddDiscountServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            return services;
        }
    }
}
