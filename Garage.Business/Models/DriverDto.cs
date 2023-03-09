namespace Garage.Business.Models;

/// <summary>
/// A class for driver data transfer object.
/// </summary>
public class DriverDto
{
	/// <summary>
	/// The id of the driver.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// The first name of the driver.
	/// </summary>
	public string FirstName { get; set; } = string.Empty;

	/// <summary>
	/// The last name of the driver.
	/// </summary>
	public string LastName { get; set; } = string.Empty;

	/// <summary>
	/// The eye color of the driver.
	/// </summary>
	public string EyeColor { get; set; } = string.Empty;

	/// <summary>
	/// The company the driver works for.
	/// </summary>
	public string Company { get; set; } = string.Empty;

	/// <summary>
	/// Birth date fo the driver.
	/// </summary>
	public DateTime BirthDate { get; set; }

	/// <summary>
	/// The city the driver comes from.
	/// </summary>
	public string City { get; set; } = string.Empty;
}
