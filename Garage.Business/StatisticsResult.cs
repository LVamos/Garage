using Garage.Data.Models;

namespace Garage.Business;

public class StatisticsResult
{
	/// <summary>
	/// The average age of the drivers.
	/// </summary>
	public int AverageAge { get; set; }

	/// <summary>
	/// Drivers grouped by birth year.
	/// </summary>
	public Dictionary<int, int>? DriversByBirthYear { get; set; }

	/// <summary>
	/// Drivers grouped by last name.
	/// </summary>
	public Dictionary<string, int[]>? DriversByLastName { get; set; }

	/// <summary>
	/// Three most frequent brand names.
	/// </summary>
	public string[]? MostFrequentBrandNames { get; set; }

	/// <summary>
	/// Average age of the vehicles by brand name.
	/// </summary>
	public Dictionary<string, int>? AverageVehicleAgeByBrandName { get; set; }

	/// <summary>
	/// Percentage of drivers driving vehicles with a specific engine type.
	/// </summary>
	public Dictionary<EngineType, double>? EngineTypePercentage { get; set; }

	/// <summary>
	/// Ids of blue-eyed drivers using wehicles with hybrid or electric engines.
	/// </summary>
	public int[]? BlueEyedDriversWithHybridOrElectricsVehicles { get; set; }

	/// <summary>
	/// Ids of drivers that have multiple vehicles and whose vehicles have all the same engine type.
	/// </summary>
	public int[]? DriversWithSameEngineType { get; set; }
}
