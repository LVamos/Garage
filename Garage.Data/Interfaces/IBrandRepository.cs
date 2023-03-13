using Garage.Data.Models;

namespace Garage.Data.Interfaces;

/// <summary>
/// An interface for a repository of vehicle brands.
/// </summary>
public interface IBrandRepository : IBaseRepository<Brand>
{
	/// <summary>
	/// Finds a brand by its name.
	/// </summary>
	/// <param name="name">Name of the requested brand</param>
	/// <returns>The wanted brand or null</returns>
	Brand? FindByName(string name);
}
