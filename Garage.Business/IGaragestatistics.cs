using Garage.Business.Models;

namespace Garage.Business;

/// <summary>
/// An interface for the GarageStatistics class.
/// </summary>
public interface IGaragestatistics
{
	/// <summary>
	/// Calculates the statistics of the garage.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returns>A StatisticsResult object</returns>
	StatisticsResult? CalculateStatistics(DriverVehiclesDto[] info);


}
