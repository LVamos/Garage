using AutoMapper;

using Garage.Business.Interfaces;
using Garage.Business.Models;
using Garage.Data;
using Garage.Data.Interfaces;
using Garage.Data.Models;

namespace Garage.Business.Managers;

/// <summary>
///  A brand manager.
/// </summary>
public class DriverVehiclesManager : IDriverVehiclesManager
{
	/// <summary>
	/// Saves DriverVehicles records into the database.
	/// </summary>
	/// <param name="entries">The DriverVehicles DTOs to be saved</param>
	/// <returns>List of DriverVehicles records or null</returns>
	public IList<DriverVehiclesDto> AddDriverVehiclesRecords(DriverVehiclesDto[] entries)
	{
		// Validate brand IDs.
		HashSet<int> invalidIds = new();
		IEnumerable<int> brandIds = entries!.SelectMany(e => e!.VehicleInfo)!.Select(v => v!.BrandId);
		foreach (int id in brandIds)
		{
			if (!invalidIds.Contains(id) && _brandManager.GetBrand(id) is null)
				invalidIds.Add(id);
		}

		if (invalidIds.Count > 0)
			throw new BrandsNotFoundException("These brand ids are invalid", invalidIds.ToArray());

		// Empty all the database except the Brands table for test purposes.
		EmptyDatabase();

		// Store the data.
		List<DriverVehiclesDto> addedEntries = new();

		foreach (DriverVehiclesDto entry in entries)
		{
			// Add the driver.
			//DriverDto? newDriverDto = _driverManager.AddDriver(entry.Driver);
			//if (newDriverDto is null)
			//	throw new InvalidOperationException("Unable to add a drive into the database.");

			// Add the vehicles.
			//IList<VehicleInfoDto?> newVehicles = new List<VehicleInfoDto?>();
			//foreach (VehicleInfoDto? vehicle in entry.VehicleInfo)
			//{
			//	VehicleInfoDto? newVehicle = _vehicleManager.AddVehicle(vehicle);
			//	if (newVehicle is null)
			//		throw new InvalidOperationException("Unable to add a vehicle into the database.");

			//	//newVehicle.Brand = null;
			//	newVehicles.Add(newVehicle);
			//}

			// Store the entry.
			DriverVehiclesDto? driverVehiclesDto = new DriverVehiclesDto()
			{
				Id = entry.Id,
				Driver = entry.Driver,
				VehicleInfo = entry.VehicleInfo
			};


			DriverVehiclesDto? newDriverVehiclesDto = AddDriverVehicles(driverVehiclesDto);
			if (newDriverVehiclesDto is null)
				throw new InvalidOperationException("Unable to save a DriverVehicles record into the database.");

			//newDriverVehiclesDto.Driver = newDriverDto;
			addedEntries.Add(newDriverVehiclesDto);
		}

		return addedEntries;
	}

	/// <summary>
	/// Empties all tables except the Brands.
	/// </summary>
	private void EmptyDatabase()
	{
		// Empty Drivervehicles table.
		_garageDbContext.DriverVehicles.RemoveRange(_garageDbContext.DriverVehicles);
		_garageDbContext.drivers.RemoveRange(_garageDbContext.drivers);
		_garageDbContext.vehicles.RemoveRange(_garageDbContext.vehicles);
		_garageDbContext.SaveChanges();
	}

	/// <summary>
	/// Deletes a brand.
	/// </summary>
	/// <param name="id">Id of the brand to be deleted</param>
	/// <returns>True if the brand was deleted</returns>
	public bool DeleteDriverVehicles(int id)
	{
		try
		{
			_drivervehiclesRepository.Delete(id);
		}
		catch
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Adds a DriverVehicles record.
	/// </summary>
	/// <param name="driverId">Id of a driver</param>
	/// <param name="vehicleIds">An array of vehicle ids</param>
	/// <returns>A DTO object storing the newly added Drivervehicles Record</returns>
	public DriverVehiclesDto? AddDriverVehicles(int driverId, int[] vehicleIds)
	{
		// Find the driver and the vehicles.
		DriverDto? driver = _driverManager.GetDriver(driverId);

		List<VehicleInfoDto> vehicles = new();
		foreach (int id in vehicleIds)
		{
			VehicleInfoDto? vehicle = _vehicleManager.GetVehicle(id);
			if (vehicle is null)
				return null;

			vehicles.Add(vehicle);
		}

		if (driver is null || vehicles.Count == 0)
			return null;

		// Store the brand.
		DriverVehiclesDto driverVehiclesDto = new DriverVehiclesDto()
		{
			Driver = driver,
			VehicleInfo = vehicles
		};

		return AddDriverVehicles(driverVehiclesDto);
	}

	/// <summary>
	/// Adds a DriverVehicles record.
	/// </summary>
	/// <param name="driverVehiclesDto"A DTO describing the DriverVehicles record to be added></param>
	/// <returns>Newly added Drivervehicles record as a DTO</returns>
	public DriverVehiclesDto? AddDriverVehicles(DriverVehiclesDto driverVehiclesDto)
	{
		DriverVehicles driverVehicles = _mapper.Map<DriverVehicles>(driverVehiclesDto);
		driverVehicles.Vehicles = new List<Vehicle>();

		// Map the Vehicle list.
		foreach (VehicleInfoDto? v in driverVehiclesDto.VehicleInfo)
			driverVehicles!.Vehicles!.Add(new Vehicle { BrandId = v!.BrandId, ModelYear = v!.ModelYear });

		DriverVehicles? newDriverVehicles = _drivervehiclesRepository.Insert(driverVehicles);
		if (newDriverVehicles is null)
			return null;

		DriverVehiclesDto result = _mapper.Map<DriverVehiclesDto>(newDriverVehicles);

		// Map the Vehicle list.
		foreach (Vehicle v in newDriverVehicles.Vehicles)
			result.VehicleInfo.Add(_mapper.Map<VehicleInfoDto>(v));

		return result;
	}

	/// <summary>
	///  Returns all brands.
	/// </summary>
	/// <returns>A list of brands</returns>
	public IList<DriverVehiclesDto>? GetAllDriverVehicless()
	{
		IList<DriverVehicles> brands = _drivervehiclesRepository.GetAll();
		return _mapper.Map<IList<DriverVehiclesDto>>(brands);
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="driverVehiclesRepository">The brand repository</param>
	/// <param name="mapper">The mapper</param>
	public DriverVehiclesManager(IDriverVehiclesRepository driverVehiclesRepository, IDriverManager driverManager, IVehicleManager vehicleManager, IBrandManager brandManager, IMapper mapper, GarageDbContext garageDbContext)
	{
		_drivervehiclesRepository = driverVehiclesRepository;
		_driverManager = driverManager;
		_vehicleManager = vehicleManager;
		_brandManager = brandManager;
		_mapper = mapper;
		_garageDbContext = garageDbContext;
	}

	/// <summary>
	///  Gets a brand.
	/// </summary>
	/// <param name="id">Id of the branded brand</param>
	/// <returns>A data transformation object</returns>
	public DriverVehiclesDto? GetDriverVehicles(int id)
	{
		DriverVehicles? brand = _drivervehiclesRepository.FindById(id);

		if (brand is null)
			return null;

		return _mapper.Map<DriverVehiclesDto>(brand);
	}


	/// <summary>
	///  The brand repository.
	/// </summary>
	private readonly IDriverVehiclesRepository _drivervehiclesRepository;

	/// <summary>
	/// A driver manager
	/// </summary>
	private readonly IDriverManager _driverManager;

	/// <summary>
	/// A vehicle manager
	/// </summary>
	private readonly IVehicleManager _vehicleManager;

	/// <summary>
	/// A brand manager
	/// </summary>
	private readonly IBrandManager _brandManager;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper;

	/// <summary>
	/// Context of the current database.
	/// </summary>
	private readonly GarageDbContext _garageDbContext;
}