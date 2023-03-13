using AutoMapper;

using Garage.Business.Interfaces;
using Garage.Business.Models;
using Garage.Data.Interfaces;
using Garage.Data.Models;

namespace Garage.Business.Managers;

/// <summary>
///  A vehicle manager.
/// </summary>
public class VehicleManager : IVehicleManager
{
	/// <summary>
	/// Deletes a vehicle.
	/// </summary>
	/// <param name="id">Id of the vehicle to be deleted</param>
	/// <returns>True if the vehicle was deleted</returns>
	public bool DeleteVehicle(int id)
	{
		try
		{
			_vehicleRepository.Delete(id);
		}
		catch
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Adds a vehicle.
	/// </summary>
	/// <param name="brandName">Name of the vehicle.</param>
	/// <param name="modelYear">Model year of the vehicle</param>
	/// <param name="engineType">Engine type of the vehicle</param>
	/// <returns>A DTO object storing the newly added vehicle</returns>
	public VehicleInfoDto? AddVehicle(string brandName, int modelYear, EngineType engineType)
	{
		// Find the brand id.
		BrandDto? brand = _brandManager.GetBrandByName(brandName);
		if (brand is null)
			return null;

		// Store the vehicle.
		VehicleInfoDto vehicle = new VehicleInfoDto()
		{
			BrandId = brand.Id,
			EngineType = engineType,
			ModelYear = modelYear
		};

		return AddVehicle(vehicle);
	}

	/// <summary>
	///  Adds a vehicle.
	/// </summary>
	/// <param name="vehicleDto">The vehicle as an DTO object to be added</param>
	/// <returns>Newly added vehicle as an DTO object</returns>
	public VehicleInfoDto? AddVehicle(VehicleInfoDto vehicleDto)
	{
		// Check if the brand exists.
		BrandDto? brand = _brandManager.GetBrand(vehicleDto.BrandId);
		if (brand is null)
			throw new ArgumentException($"Brand {vehicleDto.BrandId}does not exist.");

		vehicleDto.Brand = null;
		Vehicle vehicle = _mapper.Map<Vehicle>(vehicleDto);
		Vehicle newVehicle = _vehicleRepository.Insert(vehicle);

		return _mapper.Map<VehicleInfoDto?>(newVehicle);
	}

	/// <summary>
	///  Returns all vehicles.
	/// </summary>
	/// <returns>A list of vehicles</returns>
	public IList<VehicleInfoDto> GetAllVehicles()
	{
		IList<Vehicle> vehicles = _vehicleRepository.GetAll();
		return _mapper.Map<IList<VehicleInfoDto>>(vehicles);
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="vehicleRepository">The vehicle repository</param>
	/// <param name="mapper">The mapper</param>
	public VehicleManager(IVehicleRepository vehicleRepository, IBrandManager brandManager, IMapper mapper)
	{
		_vehicleRepository = vehicleRepository;
		_brandManager = brandManager;
		_mapper = mapper;
	}

	/// <summary>
	///  Gets a vehicle.
	/// </summary>
	/// <param name="id">Id of the vehicleed vehicle</param>
	/// <returns>A data transformation object</returns>
	public VehicleInfoDto? GetVehicle(int id)
	{
		Vehicle? vehicle = _vehicleRepository.FindById(id);

		if (vehicle is null)
			return null;

		return _mapper.Map<VehicleInfoDto>(vehicle);
	}

	/// <summary>
	/// Finds all vehicles with the specified brand name.
	/// </summary>
	/// <param name="brandName">Brand name of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	public IList<VehicleInfoDto>? FindByBrandName(string brandName)
	{
		IList<Vehicle>? vehicles = _vehicleRepository.FindByBrandName(brandName);
		return _mapper.Map<IList<VehicleInfoDto>>(vehicles);
	}

	/// <summary>
	/// Finds all vehicles with the specified engine type.
	/// </summary>
	/// <param name="engineType">Engine type of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	public IList<VehicleInfoDto>? FindByEngineType(EngineType engineType)
	{
		IList<Vehicle>? vehicles = _vehicleRepository.FindByEngineType(engineType);
		return _mapper.Map<IList<VehicleInfoDto>>(vehicles);
	}

	/// <summary>
	/// Finds all vehicles with the specified model year.
	/// </summary>
	/// <param name="modelYear">Model year of the requested vehicles</param>
	/// <returns>List of vehicles or null</returns>
	public IList<VehicleInfoDto>? FindByModelYear(int modelYear)
	{
		IList<Vehicle>? vehicles = _vehicleRepository.FindByModelYear(modelYear);
		return _mapper.Map<IList<VehicleInfoDto>>(vehicles);
	}

	/// <summary>
	///  The vehicle repository.
	/// </summary>
	private readonly IVehicleRepository _vehicleRepository;

	/// <summary>
	/// A brand manager.
	/// </summary>
	private readonly IBrandManager _brandManager;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper;
}