using AutoMapper;

using Garage.Business.Interfaces;
using Garage.Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace Garage.Controllers;

/// <summary>
/// A controller for drivers management.
/// </summary>
[Route("api")]
[ApiController]
public class DriversController : Controller
{
	/// <summary>
	/// Returns all drivers.
	/// </summary>
	/// <returns>An enumeration of driver DTOs</returns>
	[HttpGet("drivers")]
	public IActionResult GetDrivers()
	{
		IEnumerable<DriverDto>? drivers = _drivermanager.GetAllDrivers();

		return drivers.Any() ? Ok(drivers) : NotFound("No data");
	}

	/// <summary>
	/// Returns all drivers with the specified birth date.
	/// </summary>
	/// <param name="firstName">Birth date of the requested drivers</param>
	/// <returns>Enumeration of drivers</returns>
	[HttpGet("driversbybirthyear")]
	public IActionResult GetDriversByBirthYear(int birthYear)
	{
		IEnumerable<DriverDto>? drivers = _drivermanager.FindByBirthYear(birthYear);

		return drivers.Any() ? Ok(drivers) : NotFound("No data");
	}

	/// <summary>
	/// Returns all drivers with the specified eye color.
	/// </summary>
	/// <param name="eyeColor">Eye color of the requested drivers</param>
	/// <returns>Enumeration of drivers</returns>
	[HttpGet("driversbyeyecolor")]
	public IActionResult GetDriversByEyeColor(string eyeColor)
	{
		IEnumerable<DriverDto>? drivers = _drivermanager.FindByEyeColor(eyeColor);

		return drivers.Any() ? Ok(drivers) : NotFound("No data");
	}

	/// <summary>
	/// Returns all drivers from the specified city.
	/// </summary>
	/// <param name="city">A city the requested drivers live in</param>
	/// <returns>Enumeration of drivers</returns>
	[HttpGet("driversbycity")]
	public IActionResult GetDriversByCity(string city)
	{
		IEnumerable<DriverDto>? drivers = _drivermanager.FindByCity(city);

		return drivers.Any() ? Ok(drivers) : NotFound("No data");
	}

	/// <summary>
	/// Returns all drivers from the specified company.
	/// </summary>
	/// <param name="company">A company the requested drivers work for</param>
	/// <returns>Enumeration of drivers</returns>
	[HttpGet("driversbycompany")]
	public IActionResult GetDriversByCompany(string company)
	{
		IEnumerable<DriverDto>? drivers = _drivermanager.FindByCompany(company);

		return drivers.Any() ? Ok(drivers) : NotFound("No data");
	}

	/// <summary>
	/// Returns all drivers with the specified name.
	/// </summary>
	/// <param name="firstName">First name of the requested drivers</param>
	/// <param name="lastName">Last name of the requested drivers</param>
	/// <returns>Enumeration of drivers</returns>
	[HttpGet("driversbyname")]
	public IActionResult GetDriversByName(string firstName, string lastName)
	{
		IEnumerable<DriverDto>? drivers = _drivermanager.FindByName(firstName, lastName);

		return drivers.Any() ? Ok(drivers) : NotFound("No data");
	}

	/// <summary>
	/// Returns a driver with the specified ID.
	/// </summary>
	/// <param name="id">ID of the requested drivers</param>
	/// <returns>A driver DTO</returns>
	[HttpGet("drivers/{id}")]
	public IActionResult GetDriver(int id)
	{
		DriverDto? driver = _drivermanager.GetDriver(id);

		return driver is not null ? Ok(driver) : NotFound("No data");
	}

	/// <summary>
	/// A driver manager.
	/// </summary>
	private readonly IDriverManager _drivermanager;

	/// <summary>
	/// A mapper for converting between DTO and model classes.
	/// </summary>
	private readonly IMapper _mapper;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="driverManager">A driver manager</param>
	public DriversController(IDriverManager driverManager, IMapper mapper)
	{
		_drivermanager = driverManager;
		_mapper = mapper;
	}
}
