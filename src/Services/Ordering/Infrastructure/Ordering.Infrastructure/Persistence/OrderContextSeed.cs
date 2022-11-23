using Ordering.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreConfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() 
                {
                    UserName = "Ahmed", 
                    FirstName = "Ahmed", 
                    LastName = "Samir", 
                    EmailAddress = "ezozkme@gmail.com", 
                    AddressLine = "Baghdad", 
                    Country = "Iraq", 
                    TotalPrice = 350M, 
                    CVV = "None", 
                    CardName = "None",
                    CardNumber = "None",
                    Expiration = "None",
                    PaymentMethod = 1,
                    State = "None",
                    ZipCode = "None",
                    CreatedBy = "None",
                    CreatedDate = DateTime.Now,
                    LastModifiedBy = "None",
                    LastModifiedDate = DateTime.Now
                }
            };
        }
    }
}
