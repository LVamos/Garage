using Garage.Business.Models;

namespace Garage.Business.Interfaces;

/// <summary>
/// An interface for brand manager.
/// </summary>
public interface IBrandManager
{
	/// <summary>
	/// Adds a brand.
	/// </summary>
	/// <param name="name">Name of the brand.</param>
	/// <returns>A DTO object storing the newly added brand</returns>
	BrandDto? AddBrand(string name);

	/// <summary>
	///  Adds a brand.
	/// </summary>
	/// <param name="brandDto">The brand as an DTO object to be added</param>
	/// <returns>Newly added brand as an DTO object</returns>
	BrandDto AddBrand(BrandDto brandDto);

	/// <summary>
	/// Deletes a brand.
	/// </summary>
	/// <param name="id">Id of the brand to be deleted</param>
	/// <returns>True if the brand was deleted</returns>
	bool DeleteBrand(int id);

	/// <summary>
	///  Returns all brands.
	/// </summary>
	/// <returns>A list of brands</returns>
	IList<BrandDto>? GetAllBrands();

	/// <summary>
	///  Gets a brand.
	/// </summary>
	/// <param name="id">Id of the branded brand</param>
	/// <returns>A data transformation object</returns>
	BrandDto? GetBrand(int id);

	/// <summary>
	/// Gets a brand by name.
	/// </summary>
	/// <param name="name">name of the brand to be found</param>
	/// <returns>The requested brand DTO or null</returns>
	BrandDto? GetBrandByName(string name);
}
