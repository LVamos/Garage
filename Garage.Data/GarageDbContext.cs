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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Brand>().HasData(
			new Brand
			{
				Id = 0,
				Name = "BMW"
			},

			new Brand
			{
				Id = 1,
				Name = "Audi"
			},

			new Brand()
			{
				Id = 2,
				Name = "Mercedes"
			},

			new Brand
			{
				Id = 3,
				Name = "Skoda"
			},

			new Brand
			{
				Id = 4,
				Name = "Fiat"
			},

			new Brand()
			{
				Id = 5,
				Name = "Renault"
			},

			new Brand { Id = 6, Name = "Lexus" },
			new Brand { Id = 7, Name = "Ferrari" },
			new Brand { Id = 8, Name = "Porsche" },
			new Brand { Id = 9, Name = "Kia" }
			);
	}
}
