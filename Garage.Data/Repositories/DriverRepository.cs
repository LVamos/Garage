using Garage.Data.Interfaces;
using Garage.Data.Models;

namespace Garage.Data.Repositories;

/// <summary>
/// A repository for vehicle drivers.
/// </summary>
public class DriverRepository : BaseRepository<Driver>, IDriverRepository
{
	/// <summary>
	/// Finds a driver.
	/// </summary>
	/// <param name="firstName">First name of the searched driver</param>
	/// <param name="lastName">Last name  of the searched driver</param>
	/// <param name="birthDate">Birth date  of the searched driver</param>
	/// <param name="city">City the searched driver lives in</param>
	/// <param name="company">Company  the searched driver works for</param>
	/// <param name="eyeColor">Eye color  of the searched driver</param>
	/// <returns>The requested driver or null</returns>
	public Driver? Find(string firstName, string lastName, DateTime birthDate, string city, string company, string eyeColor)
	{
		return
			_dbSet
			.SingleOrDefault(d => d.FirstName == firstName && d.LastName == lastName && d.BirthDate == birthDate && d.City == city && d.Company == company && d.EyeColor == eyeColor);
	}

	/// <summary>
	/// Finds all drivers with specified name.
	/// </summary>
	/// <param name="firstName">First name of the requested driver</param>
	/// <param name="lastName">Last name of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	public IList<Driver>? FindByName(string firstName, string lastName)
	{
		return
			_dbSet
			.Where(d => d.FirstName == firstName && d.LastName == lastName)
			.ToList();
	}

	/// <summary>
	/// Finds all drivers with specified birth year.
	/// </summary>
	/// <param name="birthYear">Birth year of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	public IList<Driver>? FindByBirthYear(int birthYear)
	{
		return
	_dbSet
	.Where(d => d.BirthDate.Year == birthYear)
	.ToList();
	}

	/// <summary>
	/// Finds all drivers from specified city.
	/// </summary>
	/// <param name="city">The city the drivers come from</param>
	/// <returns>List of drivers or null</returns>
	public IList<Driver>? FindByCity(string city)
	{
		return
	_dbSet
	.Where(d => d.City == city)
	.ToList();
	}

	/// <summary>
	/// Finds all drivers from specified company.
	/// </summary>
	/// <param name="company">The company the drivers work for</param>
	/// <returns>List of drivers or null</returns>
	public IList<Driver>? FindByCompany(string company)
	{
		return
	_dbSet
	.Where(d => d.Company == company)
	.ToList();
	}

	/// <summary>
	/// Finds all drivers with specified eye color.
	/// </summary>
	/// <param name="eyeColor">Eye color of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	public IList<Driver>? FindByEyeColor(string eyeColor)
	{
		return
	_dbSet
	.Where(d => d.EyeColor == eyeColor)
	.ToList();
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="dbContext">Context of a database to work with</param>
	public DriverRepository(GarageDbContext dbContext) : base(dbContext) { }
}
