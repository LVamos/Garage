using AutoMapper;

using Garage.Business;
using Garage.Business.Interfaces;
using Garage.Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace Garage.Controllers;

/// <summary>
/// A controller for garage database management.
/// </summary>
[Route("api")]
[ApiController]
[FormatFilter]
public class DataController : Controller
{
	/// <summary>
	/// Gets all drivers.
	/// </summary>
	/// <param name="entries"></param>
	/// <returns></returns>
	[HttpPost("data.{format}")]
	public IActionResult SaveData(DriverVehiclesDto[] entries)
	{
		IList<DriverVehiclesDto>? result = null;

		try
		{
			result = _driverVehiclesManager.AddDriverVehiclesRecords(entries);
		}
		catch (BrandsNotFoundException ex)
		{
			return BadRequest(new { ex.Message, ex.InvalidBrandIds });
		}
		catch (Exception ex)
		{
			return Problem(title: "Error", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
		}

		return Ok(result);
	}

	/// <summary>
	/// A manager for DriverVehicles records.
	/// </summary>
	private readonly IDriverVehiclesManager _driverVehiclesManager;

	/// <summary>
	/// A mapper for converting between DTO and model classes.
	/// </summary>
	private readonly IMapper _mapper;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="garagestatistics">The class for statistics.</param>
	public DataController(IDriverVehiclesManager drivervehiclesManager, IMapper mapper)
	{
		_driverVehiclesManager = drivervehiclesManager;
		_mapper = mapper;
	}
}
