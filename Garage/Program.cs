using Garage;
using Garage.Business;
using Garage.Business.Interfaces;
using Garage.Business.Managers;
using Garage.Data;
using Garage.Data.Interfaces;
using Garage.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;

using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile));
builder.Services.AddScoped<IGaragestatistics, GarageStatistics>();

// Repositories and managers.
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandManager, BrandManager>();

builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IDriverManager, DriverManager>();

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleManager, VehicleManager>();

builder.Services.AddScoped<IDriverVehiclesRepository, DriverVehiclesRepository>();
builder.Services.AddScoped<IDriverVehiclesManager, DriverVehiclesManager>();




// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
	o =>
	o.SwaggerDoc("garage", new OpenApiInfo
	{
		Version = "v1",
		Title = "Garage",
		Description = "Web API for garage management"
	}));

// Prepare database.
string? connectionString = builder.Configuration.GetConnectionString("LocalStargateConnection");
builder.Services.AddDbContext<GarageDbContext>(
	o => o.UseSqlServer(connectionString)
	.UseLazyLoadingProxies()
	.ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)));

// Controllers
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
	.AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())).AddXmlSerializerFormatters();

WebApplication app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(o =>
{
	o.SwaggerEndpoint("garage/swagger.json", "Garage - v1");
});

// Run migrations at startup.
{
	using IServiceScope scope = app.Services.CreateScope();
	IServiceProvider services = scope.ServiceProvider;
	GarageDbContext context = services.GetRequiredService<GarageDbContext>();
	context.Database.Migrate();
}

app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.Run();
