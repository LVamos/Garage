using AutoMapper;

using Garage.Business.Models;
using Garage.Data;
using Garage.Data.Models;

namespace Garage.Business;

/// <summary>
/// The statistics of the garage.
/// </summary>
public class GarageStatistics : IGaragestatistics
{
	/// <summary>
	/// Context for the database.
	/// </summary>
	private GarageDbContext _dbContext;

	/// <summary>
	/// Automapper instance.
	/// </summary>
	private readonly IMapper _mapper;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="dbContext">Context for the database</param>
	/// <param name="mapper">An automapper instance</param>
	public GarageStatistics(GarageDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	/// <summary>
	/// Calculates the statistics of the garage.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returns>A StatisticsResult object</returns>
	public StatisticsResult? CalculateStatistics(DriverVehiclesDto[] info)
	{
		IEnumerable<VehicleInfoDto> vehicles = info.SelectMany(i => i.VehicleInfo);

		// Validate brand ids.
		HashSet<int> invalidIds = new();
		foreach (int id in vehicles.Select(v => v.BrandId))
		{
			if (!invalidIds.Contains(id) && _dbContext.Brands.Find(id) is null)
				invalidIds.Add(id);
		}

		if (invalidIds.Count() > 0)
			throw new BrandsNotFoundException("You have entered innvalid brand ids", invalidIds.ToArray<int>());

		// Store brand references.
		foreach (VehicleInfoDto vehicle in vehicles)
		{
			Brand? brand = _dbContext.Brands.Find(vehicle.BrandId);
			vehicle.Brand = _mapper.Map<BrandDto>(brand);
		}

		// Set ids of drivers.
		foreach (DriverVehiclesDto i in info)
			i.Driver.Id = i.Id;

		// Calculate statistics.
		return new()
		{
			AverageAge = GetAverageAge(info),
			DriversByBirthYear = GetDriversByBirthYear(info),
			DriversByLastName = GetDriversByLastName(info),
			MostFrequentBrandNames = GetMostFrequentBrandNames(info),
			AverageVehicleAgeByBrandName = GetAverageVehicleAgeByBrandName(info),
			EngineTypePercentage = GetEngineTypePercentage(info),
			BlueEyedDriversWithHybridOrElectricsVehicles = GetBlueEyedDrivers(info),
			DriversWithSameEngineType = GetDriversWithSameEngineType(info)
		};
	}

	/// <summary>
	/// Returns ids of drivers that have multiple vehicles and whose vehicles have all the same engine type.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returns>Ids of drivers that have multiple vehicles and whose vehicles have all the same engine type.</returns>
	private int[] GetDriversWithSameEngineType(DriverVehiclesDto?[] info)
	{
		List<int> result = new();

		// Query drivers with multiple vehicles.
		IEnumerable<DriverVehiclesDto> drivers = info.Where(d => d.VehicleInfo.Count() > 1);
		foreach (DriverVehiclesDto i in drivers)
		{
			// Collect drivers with vehicles of same engines.
			EngineType firstEngine = i.VehicleInfo.First().EngineType;

			if (i.VehicleInfo.All(v => v?.EngineType == firstEngine))
				result.Add(i!.Driver!.Id);
		}

		return result.ToArray<int>();
	}

	/// <summary>
	/// Returns ids of blue-eyed drivers using wehicles with hybrid or electric engines.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returns>Ids of blue-eyed drivers using wehicles with hybrid or electric engines.</returns>
	private int[] GetBlueEyedDrivers(DriverVehiclesDto[] info)
	{
		return
					info.Where(i => i.Driver.EyeColor == "blue")
					.Where(i => i.VehicleInfo.Any(v => v.EngineType == EngineType.Electric || v.EngineType == EngineType.Hybrid))
					.Select(i => i.Driver.Id)
					.ToArray<int>();
	}

	/// <summary>
	/// Returns percentage of drivers driving vehicles with a specific engine type.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returns>Percentage of drivers driving vehicles with a specific engine type.</returns>
	private Dictionary<EngineType, double> GetEngineTypePercentage(DriverVehiclesDto[] info)
	{
		Dictionary<EngineType, double> result = new();

		foreach (EngineType engine in Enum.GetValues(typeof(EngineType)))
		{
			int engineCount =
							info
							.Where(i => i.VehicleInfo.Any(v => v.EngineType == engine))
							.Count();

			result[engine] = engineCount / (info.Length / 100);
		}

		return result;
	}

	/// <summary>
	/// Calculates age from teh specified date.
	/// </summary>
	/// <param name="date"></param>
	/// <returns></returns>
	private int GetAge(DateTime date)
	{
		double years = (DateTime.Now - date).TotalDays / 365.25f;
		return (int)(Math.Round(years));
	}


	/// <summary>
	/// Returns average age of the vehicles by brand name.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returns>The average age of the vehicles by brand name.</returns>
	private Dictionary<string, int>? GetAverageVehicleAgeByBrandName(DriverVehiclesDto[] info)
	{
		// Get all brands.
		IEnumerable<string> brandnames =
		info.SelectMany(i => i!.VehicleInfo!)!
			.Select(v => v!.Brand!.Name!)
			.Distinct();

		// All vehicles.
		var vehicles = info.SelectMany(i => i.VehicleInfo);

		Dictionary<string, int> vehicleAges = new Dictionary<string, int>();
		foreach (string brand in brandnames)
		{
			var vehiclesByBrand = vehicles.Where(v => v?.Brand?.Name == brand);
			double averageAge = vehiclesByBrand.Average(v => DateTime.Now.Year - v.ModelYear);

			vehicleAges[brand] = (int)Math.Round(averageAge);
		}

		return vehicleAges;
	}

	/// <summary>
	/// Returns three most frequent brand names.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returns>A string array with three most frequent brand names.</returns>
	private string[]? GetMostFrequentBrandNames(DriverVehiclesDto[] info)
	{
		IEnumerable<VehicleInfoDto> vehicles =
			info.SelectMany(i => i.VehicleInfo);

		var groups = vehicles.GroupBy(v => v.Brand.Name)
		.OrderByDescending(g => g.Count())
		.Take(3);

		return
			groups.Select(g => g.Key)
			.ToArray<string>();
	}

	/// <summary>
	/// Returns drivers grouped by last name.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returns>Drivers grouped by last name.</returns>
	private Dictionary<string, int[]> GetDriversByLastName(DriverVehiclesDto[] info)
	{
		var groups =
			info.Select(i => i.Driver)
			.GroupBy(d => d.LastName)
			.Where(g => g.Count() >= 2)
			.OrderBy(g => g.Key);

		var query =
			groups.Select(g => new { key = g.Key, ids = g.Select(d => d.Id).ToArray<int>() });

		return
			query.ToDictionary(g => g.key, g => g.ids);
	}

	/// <summary>
	/// Returns drivers grouped by birth year.
	/// </summary>
	/// <param name="info">Array of DriverVehicles DTOs</param>
	/// <returnsDrivers grouped by birth year.></returns>
	private Dictionary<int, int> GetDriversByBirthYear(DriverVehiclesDto[] info)
	{
		var groups =
		info.Select(i => i.Driver)
		.GroupBy(d => d.BirthDate.Year)
		.OrderBy(g => g.Key);

		return
			groups.Select(g => new { key = g.Key, count = g.Count() })
		.ToDictionary(g => g.key, g => g.count);
	}

	/// <summary>
	/// Gets the average age of the drivers.
	/// </summary>
	/// <param name="info">DTO array</param>
	/// <returns>Rounded average age</returns>
	private int GetAverageAge(DriverVehiclesDto[] info)
	{
		double averageAge = info.Average(i => GetAge(i.Driver.BirthDate));
		return (int)Math.Round(averageAge);
	}
}
