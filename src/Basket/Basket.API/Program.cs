var builder = WebApplication.CreateBuilder(args);

var assemblyMarker = typeof(Program).Assembly;
var connectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddTransient<IBasketRepository, BasketRepository>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assemblyMarker);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assemblyMarker);

builder.Services.AddCarter();

builder.Services.AddMarten(config =>
{
    config.Connection(connectionString);
    config.Schema.For<ShoppingCart>().Identity(sc => sc.UserName);
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();
