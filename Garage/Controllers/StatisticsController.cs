using Garage.Business;
using Garage.Business.Interfaces;
using Garage.Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace Garage.Controllers;

/// <summary>
/// A controller for garage statistics.
/// </summary>
[Route("api")]
[ApiController]
[FormatFilter]
public class StatisticsController : Controller
{
	/// <summary>
	/// The class for statistics.
	/// </summary>
	private readonly IGaragestatistics _garagestatistics;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="garagestatistics">The class for statistics.</param>
	public StatisticsController(IGaragestatistics garagestatistics) => _garagestatistics = garagestatistics;

	/// <summary>
	/// Calculates the statistics of the garage.
	/// </summary>
	/// <param name="entries">An array of driverVehiclesDto objects</param>
	/// <param name="minId">Lower bound of driver selection</param>
	/// <param name="maxId">Upper bound for driver selection</param>
	/// <returns>IActionResult</returns>
	[HttpPost("statistics.{format}")]
	public IActionResult CalculateStatistics([FromBody] DriverVehiclesDto[] entries, int? minId, int? maxId)
	{
		StatisticsResult? result = null;

		try
		{
			result = _garagestatistics.CalculateStatistics(entries, minId, maxId);
		}
		catch (BrandsNotFoundException e)
		{
			return BadRequest(new { e.Message, e.InvalidBrandIds });
		}

		return Ok(result);
	}
}
