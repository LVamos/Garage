using Garage.Business.Models;

namespace Garage.Business.Interfaces;

/// <summary>
/// An interface for driver manager.
/// </summary>
public interface IDriverManager
{
	/// <summary>
	/// Adds a driver.
	/// </summary>
	/// <param name="firstName">First name of the driver.</param>
	/// <param name="lastName">Last name of the driver</param>
	/// <param name="birthDate">Birth date of the driver</param>
	/// <param name="city">City the driver lives in</param>
	/// <param name="company">The company the driver works for</param>
	/// <param name="eyeColor">Eye color of the driver</param>
	/// <returns>A DTO object storing the newly added driver</returns>
	DriverDto? AddDriver(string firstName, string lastName, DateTime birthDate, string city, string company, string eyeColor);

	/// <summary>
	///  Adds a driver.
	/// </summary>
	/// <param name="driverDto">The driver as an DTO object to be added</param>
	/// <returns>Newly added driver as an DTO object</returns>
	DriverDto AddDriver(DriverDto driverDto);

	/// <summary>
	/// Deletes a driver.
	/// </summary>
	/// <param name="id">Id of the driver to be deleted</param>
	/// <returns>True if the driver was deleted</returns>
	bool DeleteDriver(int id);

	/// <summary>
	///  Returns all drivers.
	/// </summary>
	/// <returns>A list of drivers</returns>
	IList<DriverDto>? GetAllDrivers();

	/// <summary>
	///  Gets a driver.
	/// </summary>
	/// <param name="id">Id of the drivered driver</param>
	/// <returns>A data transformation object</returns>
	DriverDto? GetDriver(int id);

	/// <summary>
	/// Finds all drivers with specified name.
	/// </summary>
	/// <param name="firstName">First name of the requested driver</param>
	/// <param name="lastName">Last name of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	IList<DriverDto>? FindByName(string firstName, string lastName);

	/// <summary>
	/// Finds all drivers with specified birth year.
	/// </summary>
	/// <param name="birthYear">Birth year of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	IList<DriverDto>? FindByBirthYear(int birthYear);

	/// <summary>
	/// Finds all drivers from specified city.
	/// </summary>
	/// <param name="city">The city the drivers come from</param>
	/// <returns>List of drivers or null</returns>
	IList<DriverDto>? FindByCity(string city);

	/// <summary>
	/// Finds all drivers from specified company.
	/// </summary>
	/// <param name="company">The company the drivers work for</param>
	/// <returns>List of drivers or null</returns>
	IList<DriverDto>? FindByCompany(string company);

	/// <summary>
	/// Finds all drivers with specified eye color.
	/// </summary>
	/// <param name="eyeColor">Eye color of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	IList<DriverDto>? FindByEyeColor(string eyeColor);

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
	DriverDto? FindDriver(string firstName, string lastName, DateTime birthDate, string city, string company, string eyeColor);
}
