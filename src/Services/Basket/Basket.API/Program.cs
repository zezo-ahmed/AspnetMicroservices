using Discount.Grpc.Protos;
using Basket.API.GrpcServices;
using Basket.API.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
