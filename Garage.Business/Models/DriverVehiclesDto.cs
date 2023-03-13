namespace Garage.Business.Models;

/// <summary>
/// A class for driver and his vehicles data transfer object.
/// </summary>
public class DriverVehiclesDto
{
	/// <summary>
	/// The Id of the driver and his vehicles.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// The Id of the driver.
	/// </summary>
	public int DriverId { get; set; }

	/// <summary>
	/// A reference to a driver.
	/// </summary>
	public DriverDto Driver { get; set; } = new();

	/// <summary>
	/// References to driver's vehicles.
	/// </summary>
	public IList<VehicleInfoDto> VehicleInfo { get; set; } = new List<VehicleInfoDto>();
}
