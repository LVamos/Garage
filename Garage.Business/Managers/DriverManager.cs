using AutoMapper;

using Garage.Business.Interfaces;
using Garage.Business.Models;
using Garage.Data.Interfaces;
using Garage.Data.Models;

namespace Garage.Business.Managers;

/// <summary>
///  A driver manager.
/// </summary>
public class DriverManager : IDriverManager
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
	public DriverDto? FindDriver(string firstName, string lastName, DateTime birthDate, string city, string company, string eyeColor)
	{
		Driver? driver = _driverRepository.Find(firstName, lastName, birthDate, city, company, eyeColor);
		return _mapper.Map<DriverDto>(driver);
	}

	/// <summary>
	/// Deletes a driver.
	/// </summary>
	/// <param name="id">Id of the driver to be deleted</param>
	/// <returns>True if the driver was deleted</returns>
	public bool DeleteDriver(int id)
	{
		try
		{
			_driverRepository.Delete(id);
		}
		catch
		{
			return false;
		}
		return true;
	}

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
	public DriverDto? AddDriver(string firstName, string lastName, DateTime birthDate, string city, string company, string eyeColor)
	{
		// Store the driver.
		DriverDto driver = new DriverDto()
		{
			FirstName = firstName,
			LastName = lastName,
			BirthDate = birthDate,
			City = city,
			Company = company,
			EyeColor = eyeColor
		};
		return AddDriver(driver);
	}

	/// <summary>
	///  Adds a driver.
	/// </summary>
	/// <param name="driverDto">The driver as an DTO object to be added</param>
	/// <returns>Newly added driver as an DTO object</returns>
	public DriverDto AddDriver(DriverDto driverDto)
	{
		Driver driver = _mapper.Map<Driver>(driverDto);
		Driver newDriver = _driverRepository.Insert(driver);

		return _mapper.Map<DriverDto>(newDriver);
	}

	/// <summary>
	///  Returns all drivers.
	/// </summary>
	/// <returns>A list of drivers</returns>
	public IList<DriverDto> GetAllDrivers()
	{
		IList<Driver> drivers = _driverRepository.GetAll();
		return _mapper.Map<IList<DriverDto>>(drivers);
	}

	/// <summary>
	/// Finds all drivers with specified name.
	/// </summary>
	/// <param name="firstName">First name of the requested driver</param>
	/// <param name="lastName">Last name of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	public IList<DriverDto>? FindByName(string firstName, string lastName)
	{
		IList<Driver>? drivers = _driverRepository.FindByName(firstName, lastName);
		return _mapper.Map<IList<DriverDto>>(drivers);
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="driverRepository">The driver repository</param>
	/// <param name="mapper">The mapper</param>
	public DriverManager(IDriverRepository driverRepository, IMapper mapper)
	{
		_driverRepository = driverRepository;
		_mapper = mapper;
	}

	/// <summary>
	///  Gets a driver.
	/// </summary>
	/// <param name="id">Id of the drivered driver</param>
	/// <returns>A data transformation object</returns>
	public DriverDto? GetDriver(int id)
	{
		Driver? driver = _driverRepository.FindById(id);

		if (driver is null)
			return null;

		return _mapper.Map<DriverDto>(driver);
	}

	/// <summary>
	/// Finds all drivers with specified birth date.
	/// </summary>
	/// <param name="birthDate">Birth date of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	public IList<DriverDto>? FindByBirthDate(DateTime birthDate)
	{
		IList<Driver>? drivers = _driverRepository.FindByBirthDate(birthDate);
		return _mapper.Map<IList<DriverDto>>(drivers);
	}

	/// <summary>
	/// Finds all drivers from specified city.
	/// </summary>
	/// <param name="city">The city the drivers come from</param>
	/// <returns>List of drivers or null</returns>
	public IList<DriverDto>? FindByCity(string city)
	{
		IList<Driver>? drivers = _driverRepository.FindByCity(city);
		return _mapper.Map<IList<DriverDto>>(drivers);
	}

	/// <summary>
	/// Finds all drivers from specified company.
	/// </summary>
	/// <param name="company">The company the drivers work for</param>
	/// <returns>List of drivers or null</returns>
	public IList<DriverDto>? FindByCompany(string company)
	{
		IList<Driver>? drivers = _driverRepository.FindByCompany(company);
		return _mapper.Map<IList<DriverDto>>(drivers);
	}

	/// <summary>
	/// Finds all drivers with specified eye color.
	/// </summary>
	/// <param name="eyeColor">Eye color of the requested drivers</param>
	/// <returns>List of drivers or null</returns>
	public IList<DriverDto>? FindByEyeColor(string eyeColor)
	{
		IList<Driver>? drivers = _driverRepository.FindByEyeColor(eyeColor);
		return _mapper.Map<IList<DriverDto>>(drivers);
	}

	/// <summary>
	///  The driver repository.
	/// </summary>
	private readonly IDriverRepository _driverRepository;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper;
}