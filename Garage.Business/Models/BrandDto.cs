namespace Garage.Business.Models;

/// <summary>
/// A class for brand data transfer object.
/// </summary>
public class BrandDto
{
	/// <summary>
	/// The Id of the brand.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// The name of the brand.
	/// </summary>
	public string Name { get; set; } = string.Empty;
}
