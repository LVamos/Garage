using Garage.Business.Models;

namespace Garage.Business.Interfaces;

/// <summary>
/// An interface for Drivervehicles manager.
/// </summary>
public interface IDriverVehiclesManager
{
	/// <summary>
	/// Adds a DriverVehicles record.
	/// </summary>
	/// <param name="driverId">Id of a driver</param>
	/// <param name="vehicleIds">An array of vehicle ids</param>
	/// <returns>A DTO object storing the newly added Drivervehicles Record</returns>
	DriverVehiclesDto? AddDriverVehicles(int driverId, int[] vehicleIds);

	/// <summary>
	/// Adds a DriverVehicles record.
	/// </summary>
	/// <param name="driverVehiclesDto"A DTO describing the DriverVehicles record to be added></param>
	/// <returns>Newly added Drivervehicles record as a DTO</returns>
	DriverVehiclesDto? AddDriverVehicles(DriverVehiclesDto driverVehiclesDto);

	/// <summary>
	/// Saves DriverVehicles records into the database.
	/// </summary>
	/// <param name="entries">The DriverVehicles DTOs to be saved</param>
	/// <returns>List of DriverVehicles records or null</returns>
	IList<DriverVehiclesDto>? AddDriverVehiclesRecords(DriverVehiclesDto[] entries);

	/// <summary>
	/// Deletes a Drivervehicles Record.
	/// </summary>
	/// <param name="id">Id of the Drivervehicles Record to be deleted</param>
	/// <returns>True if the Drivervehicles Record was deleted</returns>
	bool DeleteDriverVehicles(int id);

	/// <summary>
	///  Returns all Drivervehicles Records.
	/// </summary>
	/// <returns>A list of Drivervehicles Records</returns>
	IList<DriverVehiclesDto>? GetAllDriverVehicless();

	/// <summary>
	///  Gets a Drivervehicles Record.
	/// </summary>
	/// <param name="id">Id of the Drivervehicles Record</param>
	/// <returns>A data transformation object</returns>
	DriverVehiclesDto? GetDriverVehicles(int id);
}
