using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage.Data.Models;

/// <summary>
/// The type of engine of a vehicle.
/// </summary>
public enum EngineType
{
	Electric,
	Diesel,
	Hybrid
}

/// <summary>
/// A vehicle.
/// </summary>
public class Vehicle
{
	/// <summary>
	/// The id of the vehicle.
	/// </summary>
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[System.ComponentModel.DataAnnotations.Key()]
	public int Id { get; set; }

	/// <summary>
	/// Id of the brand of the vehicle.
	/// </summary>
	[Required]
	public int BrandId { get; set; }

	/// <summary>
	/// The brand of the vehicle.
	/// </summary>
	public virtual Brand? Brand { get; set; }

	/// <summary>
	/// The year the model was made.
	/// </summary>
	public int ModelYear { get; set; }

	/// <summary>
	/// Type of the engine in the vehicle.
	/// </summary>
	public EngineType Engine { get; set; }
}
