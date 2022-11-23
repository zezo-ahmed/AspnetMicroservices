using Discount.Grpc.Services;
using Discount.Grpc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDiscountGrpcServices();

var app = builder.Build();

app.MigrateDatabase<Program>();

app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
