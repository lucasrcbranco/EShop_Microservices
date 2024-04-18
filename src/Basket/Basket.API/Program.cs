var builder = WebApplication.CreateBuilder(args);

var assemblyMarker = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assemblyMarker);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assemblyMarker);

builder.Services.AddCarter();


var app = builder.Build();

app.MapCarter();

app.Run();
