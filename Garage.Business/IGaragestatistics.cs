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
	/// <param name="minId">The lower bound of the interval that defines the set of drivers included in the calculations.</param>
	/// <param name="maxId">The upper bound of the interval that defines the set of drivers included in the calculations.</param>
	/// <returns>A StatisticsResult object</returns>
	StatisticsResult? CalculateStatistics(DriverVehiclesDto[] info, int? minId, int? maxId);


}
