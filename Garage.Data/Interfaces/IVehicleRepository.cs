using Garage.Data.Models;

namespace Garage.Data.Interfaces;

/// <summary>
/// An interface for a repository of vehicle vehicles.
/// </summary>
public interface IVehicleRepository : IBaseRepository<Vehicle>
{
	/// <summary>
	/// Finds all vehicles with the specified brand name.
	/// </summary>
	/// <param name="brandName">Brand name of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	IList<Vehicle>? FindByBrandName(string brandName);

	/// <summary>
	/// Finds all vehicles with the specified engine type.
	/// </summary>
	/// <param name="engineType">Engine type of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	IList<Vehicle>? FindByEngineType(EngineType engineType);

	/// <summary>
	/// Finds all vehicles with the specified model year.
	/// </summary>
	/// <param name="modelYear">Model year of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	IList<Vehicle>? FindByModelYear(int modelYear);
}
