using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage.Data.Models;

/// <summary>
/// The brand of a car.
/// </summary>
[Index(nameof(Brand.Name), IsUnique = true)]
public class Brand
{
	/// <summary>
	/// 	/// The id of the brand.
	/// 		/// </summary>
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[System.ComponentModel.DataAnnotations.Key()]
	public int Id { get; set; }

	/// <summary>
	/// Name of the brand.
	/// </summary>
	[StringLength(20)]
	[Required]
	public string? Name { get; set; }
}
