var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();
app.MapGrpcService<DiscountService>();

app.Run();
