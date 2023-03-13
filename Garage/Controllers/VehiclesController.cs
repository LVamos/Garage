using AutoMapper;

using Garage.Business.Interfaces;
using Garage.Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace Garage.Controllers;

/// <summary>
/// A controller for vehicles management.
/// </summary>
[Route("api")]
[ApiController]
public class VehiclesController : Controller
{
	/// <summary>
	/// Returns all vehicles.
	/// </summary>
	/// <returns>An enumeration of vehicle DTOs</returns>
	[HttpGet("vehicles")]
	public IActionResult GetVehicles()
	{
		IEnumerable<VehicleInfoDto>? vehicles = _vehiclemanager.GetAllVehicles();

		return vehicles.Any() ? Ok(vehicles) : NotFound("No data");
	}

	/// <summary>
	/// Returns all vehicles with the specified brand name.
	/// </summary>
	/// <param name="brandName">Brand name of the requested vehicles</param>
	/// <returns>Enumeration of vehicles</returns>
	[HttpGet("vehiclesbybrandname")]
	public IActionResult GetVehiclesByBrandName(string brandName)
	{
		IEnumerable<VehicleInfoDto>? vehicles = _vehiclemanager.FindByBrandName(brandName);

		return vehicles.Any() ? Ok(vehicles) : NotFound("No data");
	}

	/// <summary>
	/// Returns all vehicles with the specified model year.
	/// </summary>
	/// <param name="modelYear">Model year of the requested vehicles</param>
	/// <returns>Enumeration of vehicles</returns>
	[HttpGet("vehiclesbymodelyear")]
	public IActionResult GetVehiclesByModelYear(int modelYear)
	{
		IEnumerable<VehicleInfoDto>? vehicles = _vehiclemanager.FindByModelYear(modelYear);

		return vehicles.Any() ? Ok(vehicles) : NotFound("No data");
	}

	/// <summary>
	/// Returns a vehicle with the specified ID.
	/// </summary>
	/// <param name="id">ID of the requested drivers</param>
	/// <returns>A vehicle DTO</returns>
	[HttpGet("vehicles/{id}")]
	public IActionResult GetVehicle(int id)
	{
		VehicleInfoDto? vehicle = _vehiclemanager.GetVehicle(id);

		return vehicle is not null ? Ok(vehicle) : NotFound("No data");
	}

	/// <summary>
	/// A vehicle manager.
	/// </summary>
	private readonly IVehicleManager _vehiclemanager;

	/// <summary>
	/// A mapper for converting between DTO and model classes.
	/// </summary>
	private readonly IMapper _mapper;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vehicleManager">A vehicle manager</param>
	public VehiclesController(IVehicleManager vehicleManager, IMapper mapper)
	{
		_vehiclemanager = vehicleManager;
		_mapper = mapper;
	}
}
