using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage.Data.Models;

/// <summary>
/// The driver of one or more cars.
/// </summary>
public class Driver
{
	/// <summary>
	/// The id of the driver.
	/// </summary>
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[System.ComponentModel.DataAnnotations.Key()]
	public int Id { get; set; }

	/// <summary>
	/// The first name of the driver.
	/// </summary>
	[StringLength(20)]
	[Required]
	public string? FirstName { get; set; } = string.Empty;

	/// <summary>
	/// The last name of the driver.
	/// </summary>
	[StringLength(20)]
	[Required]
	public string? LastName { get; set; } = string.Empty;

	/// <summary>
	/// The eye color of the driver.
	/// </summary>
	[StringLength(20)]
	[Required]
	public string? EyeColor { get; set; } = string.Empty;

	/// <summary>
	/// The company the driver works for.
	/// </summary>
	[StringLength(20)]
	[Required]
	public string? Company { get; set; } = string.Empty;

	/// <summary>
	/// Birth date fo the driver.
	/// </summary>
	[Required]
	public DateTime BirthDate { get; set; }

	/// <summary>
	/// The city the driver comes from.
	/// </summary>
	[StringLength(20)]
	[Required]
	public string? City { get; set; }
}
