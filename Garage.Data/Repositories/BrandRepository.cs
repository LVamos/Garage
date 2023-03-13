using Garage.Data.Interfaces;
using Garage.Data.Models;

namespace Garage.Data.Repositories;

/// <summary>
/// A repository for vehicle brands.
/// </summary>
public class BrandRepository : BaseRepository<Brand>, IBrandRepository
{
	/// <summary>
	/// Finds a brand by its name.
	/// </summary>
	/// <param name="name">Name of the requested brand</param>
	/// <returns>The wanted brand or null</returns>
	public Brand? FindByName(string name) => _dbContext.Brands.FirstOrDefault(s => s.Name == name);

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="dbContext">Context of a database to work with</param>
	public BrandRepository(GarageDbContext dbContext) : base(dbContext) { }
}
