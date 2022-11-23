using System.Reflection;
using Discount.Grpc.Repositories;

namespace Discount.Grpc.Extensions
{
    public static class DiscountGrpcServiceRegistration
    {
        public static IServiceCollection AddDiscountGrpcServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddGrpc();

            return services;
        }
    }
}
