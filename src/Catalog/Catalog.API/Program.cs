using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);


var assemblyMarker = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assemblyMarker);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));

});

builder.Services.AddValidatorsFromAssembly(assemblyMarker);

builder.Services.AddCarter();

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();
app.Run();
