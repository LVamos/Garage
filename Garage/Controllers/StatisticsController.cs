using Garage.Business;
using Garage.Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace Garage.Controllers;

[Route("api")]
[ApiController]
[FormatFilter]
public class StatisticsController : Controller
{
	private readonly IGaragestatistics _garagestatistics;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="garagestatistics">The class for statistics.</param>
	public StatisticsController(IGaragestatistics garagestatistics) => _garagestatistics = garagestatistics;

	[HttpPost("statistics.{format}")]
	public IActionResult CalculateStatistics([FromBody] DriverVehiclesDto[] info)
	{
		StatisticsResult? result = null;

		try
		{
			result = _garagestatistics.CalculateStatistics(info);
		}
		catch (BrandsNotFoundException e)
		{
			return BadRequest(new { e.Message, e.InvalidBrandIds });
		}

		return Ok(result);
	}
}
