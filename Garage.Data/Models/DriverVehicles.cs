using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage.Data.Models;

/// <summary>
/// Combination of driver and his vehicles.
/// </summary>
public class DriverVehicles
{
	/// <summary>
	/// The Id of the driver and his vehicles.
	/// </summary>
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[System.ComponentModel.DataAnnotations.Key()]
	public int Id { get; set; }

	/// <summary>
	/// A reference to a driver.
	/// </summary>
	[Required]
	public virtual Driver? Driver { get; set; }

	/// <summary>
	/// References to driver's vehicles.
	/// </summary>
	[Required]
	public virtual ICollection<Vehicle?>? Vehicles { get; set; }
}
