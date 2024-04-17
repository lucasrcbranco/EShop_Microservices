var builder = WebApplication.CreateBuilder(args);

var assemblyMarker = typeof(Program).Assembly;
var connectionString = builder.Configuration.GetConnectionString("Database")!;

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
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(connectionString);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
