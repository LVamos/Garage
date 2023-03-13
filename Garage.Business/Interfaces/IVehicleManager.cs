using Garage.Business.Models;
using Garage.Data.Models;

namespace Garage.Business.Interfaces;

/// <summary>
/// An interface for vehicle manager.
/// </summary>
public interface IVehicleManager
{
	/// <summary>
	/// Adds a vehicle.
	/// </summary>
	/// <param name="brandName">Name of the vehicle.</param>
	/// <param name="modelYear">Model year of the vehicle</param>
	/// <param name="engineType">Engine type of the vehicle</param>
	/// <returns>A DTO object storing the newly added vehicle</returns>
	VehicleInfoDto? AddVehicle(string brandName, int modelYear, EngineType engineType);

	/// <summary>
	///  Adds a vehicle.
	/// </summary>
	/// <param name="vehicleDto">The vehicle as an DTO object to be added</param>
	/// <returns>Newly added vehicle as an DTO object</returns>
	VehicleInfoDto? AddVehicle(VehicleInfoDto vehicleDto);

	/// <summary>
	/// Deletes a vehicle.
	/// </summary>
	/// <param name="id">Id of the vehicle to be deleted</param>
	/// <returns>True if the vehicle was deleted</returns>
	bool DeleteVehicle(int id);

	/// <summary>
	///  Returns all vehicles.
	/// </summary>
	/// <returns>A list of vehicles</returns>
	IList<VehicleInfoDto>? GetAllVehicles();

	/// <summary>
	///  Gets a vehicle.
	/// </summary>
	/// <param name="id">Id of the vehicleed vehicle</param>
	/// <returns>A data transformation object</returns>
	VehicleInfoDto? GetVehicle(int id);

	/// <summary>
	/// Finds all vehicles with the specified brand name.
	/// </summary>
	/// <param name="brandName">Brand name of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	public IList<VehicleInfoDto>? FindByBrandName(string brandName);

	/// <summary>
	/// Finds all vehicles with the specified engine type.
	/// </summary>
	/// <param name="engineType">Engine type of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	IList<VehicleInfoDto>? FindByEngineType(EngineType engineType);

	/// <summary>
	/// Finds all vehicles with the specified model year.
	/// </summary>
	/// <param name="modelYear">Model year of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	IList<VehicleInfoDto>? FindByModelYear(int modelYear);
}
