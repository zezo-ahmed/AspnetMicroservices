using Npgsql;

namespace Discount.Grpc.Extensions
{
    public static class AppExtensions
    {
        private enum NumberOfSeedData { MaxLimit = 50 }
        public static void MigrateDatabase<TContext>(this WebApplication app, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var configuration = services.GetRequiredService<IConfiguration>();

                try
                {
                    logger.LogInformation("Migrating Database Now ...");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    DropTableIfExists(command);

                    CreateTable(command);

                    SeedData(command);

                    logger.LogInformation("Migrated Database!");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postgres database");
                    
                    if(retryForAvailability < (int)NumberOfSeedData.MaxLimit)
                    {
                        retryForAvailability++;
                        Thread.Sleep(3000);
                        MigrateDatabase<TContext>(app, retryForAvailability);
                    }
                }
            }
        }
        
        private static void DropTableIfExists(NpgsqlCommand command)
        {
            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();
        }

        private static void CreateTable(NpgsqlCommand command)
        {
            command.CommandText = @"CREATE TABLE Coupon(ID SERIAL PRIMARY KEY NOT NULL,
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
		                                                        Amount INT)";
            command.ExecuteNonQuery();
        }

        private static void SeedData(NpgsqlCommand command)
        {
            command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('IPhone X', 'IPhone Discount', 150);";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Samsung 10', 'Samsung Discount', 100);";
            command.ExecuteNonQuery();
        }
    }
}
