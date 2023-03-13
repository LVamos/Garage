using Garage.Data.Models;

namespace Garage.Data.Interfaces;

/// <summary>
/// An interface for a repository of vehicle drivers.
/// </summary>
public interface IDriverRepository : IBaseRepository<Driver>
{
	/// <summary>
	/// Finds all drivers with specified name.
	/// </summary>
	/// <param name="firstName">First name of the requested driver</param>
	/// <param name="lastName">Last name of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	IList<Driver>? FindByName(string firstName, string lastName);

	/// <summary>
	/// Finds all drivers with specified birth year.
	/// </summary>
	/// <param name="birthYear">Birth year of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	IList<Driver>? FindByBirthYear(int birthYear);

	/// <summary>
	/// Finds all drivers from specified city.
	/// </summary>
	/// <param name="city">The city the drivers come from</param>
	/// <returns>List of drivers or null</returns>
	IList<Driver>? FindByCity(string city);

	/// <summary>
	/// Finds all drivers from specified company.
	/// </summary>
	/// <param name="company">The company the drivers work for</param>
	/// <returns>List of drivers or null</returns>
	IList<Driver>? FindByCompany(string company);

	/// <summary>
	/// Finds all drivers with specified eye color.
	/// </summary>
	/// <param name="eyeColor">Eye color of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	IList<Driver>? FindByEyeColor(string eyeColor);

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
	Driver? Find(string firstName, string lastName, DateTime birthDate, string city, string company, string eyeColor);
}
