using Garage;
using Garage.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile));


// Prepare database.
string? connectionString = builder.Configuration.GetConnectionString("LocalStargateConnection");
builder.Services.AddDbContext<GarageDbContext>(
	o => o.UseSqlServer(connectionString)
	.UseLazyLoadingProxies()
	.ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)));

WebApplication app = builder.Build();

// Run migrations at startup.
{
	using IServiceScope scope = app.Services.CreateScope();
	IServiceProvider services = scope.ServiceProvider;
	GarageDbContext context = services.GetRequiredService<GarageDbContext>();
	context.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");
app.Run();
