using Garage.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Garage.Data;

/// <summary>
/// The database context for the garage.
/// </summary>
public class GarageDbContext : DbContext
{
	/// <summary>
	/// Brands of the awailable vehicles.
	/// </summary>
	public DbSet<Brand> Brands { get; set; }

	/// <summary>
	/// Awailable vehicles.
	/// </summary>
	public DbSet<Vehicle> vehicles { get; set; }

	/// <summary>
	/// Drivers of the vehicles.
	/// </summary>
	public DbSet<Driver> drivers { get; set; }

	/// <summary>
	/// Info about the drivers and their vehicles.
	/// </summary>
	public DbSet<DriverVehicles> DriverVehicles { get; set; }

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="options">Options for the context</param>
	public GarageDbContext(DbContextOptions<GarageDbContext> options) : base(options)
	{ }
}
