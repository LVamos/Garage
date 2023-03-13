using Garage.Data.Interfaces;
using Garage.Data.Models;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Garage.Data.Repositories;

/// <summary>
/// A repository for vehicle brands.
/// </summary>
public class DriverVehiclesRepository : BaseRepository<DriverVehicles>, IDriverVehiclesRepository
{

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="dbContext">Context of a database to work with</param>
	public DriverVehiclesRepository(GarageDbContext dbContext) : base(dbContext) { }

	public override DriverVehicles Insert(DriverVehicles entity)
	{
		ICollection<Vehicle>? vehiclesBackup = entity.Vehicles;
		entity.Vehicles = new List<Vehicle>();
		EntityEntry<DriverVehicles> entry = _dbSet.Add(entity);

		// Add the vehicles.
		foreach (Vehicle v in vehiclesBackup!)
			entry.Entity.Vehicles.Add(new Vehicle { BrandId = v.BrandId, ModelYear = v.ModelYear });
		_dbContext.SaveChanges();

		return entry.Entity;
	}
}
