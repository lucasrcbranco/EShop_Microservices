var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddDbContext<DiscountContext>(options => options.UseSqlite(connectionString));

builder.Services.AddGrpc();

var app = builder.Build();

app.UseMigration();

app.MapGrpcService<DiscountService>();

app.Run();
