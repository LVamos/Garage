using Garage.Data.Interfaces;
using Garage.Data.Models;

namespace Garage.Data.Repositories;

/// <summary>
/// A repository for vehicle brands.
/// </summary>
public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
{
	/// <summary>
	/// Finds all vehicles with the specified brand name.
	/// </summary>
	/// <param name="brandName">Brand name of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	public IList<Vehicle>? FindByBrandName(string brandName) => _dbSet?.Where(v => v.Brand!.Name == brandName)?.ToList();

	/// <summary>
	/// Finds all vehicles with the specified engine type.
	/// </summary>
	/// <param name="engineType">Engine type of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	public IList<Vehicle>? FindByEngineType(EngineType engineType) => _dbSet.Where(v => v.EngineType == engineType).ToList();

	/// <summary>
	/// Finds all vehicles with the specified model year.
	/// </summary>
	/// <param name="modelYear">Model year of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	public IList<Vehicle>? FindByModelYear(int modelYear) => _dbSet.Where(v => v.ModelYear == modelYear).ToList();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="dbContext">Context of a database to work with</param>
	public VehicleRepository(GarageDbContext dbContext) : base(dbContext) { }
}
