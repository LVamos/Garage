using Garage.Data.Models;

namespace Garage.Business.Models;

/// <summary>
/// A class for vehicle data transfer object.
/// </summary>
public class VehicleDto
{
	/// <summary>
	/// The id of the vehicle.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Id of the brand of the vehicle.
	/// </summary>
	public int BrandId { get; set; }

	/// <summary>
	/// The brand of the vehicle.
	/// </summary>
	public BrandDto? Brand { get; set; }

	/// <summary>
	/// The year the model was made.
	/// </summary>
	public int ModelYear { get; set; }

	/// <summary>
	/// Type of the engine in the vehicle.
	/// </summary>
	public EngineType Engine { get; set; }
}
