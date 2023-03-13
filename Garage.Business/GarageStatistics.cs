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
	/// <param name="entries">Array of DriverVehicles DTOs</param>
	/// <param name="minId">The lower bound of the interval that defines the set of drivers included in the calculations.</param>
	/// <param name="maxId">The upper bound of the interval that defines the set of drivers included in the calculations.</param>
	/// <returns>A StatisticsResult object</returns>
	public StatisticsResult? CalculateStatistics(DriverVehiclesDto[] entries, int? minId = null, int? maxId = null)
	{
		// Select set of drivers included in the calculations.
		DriverVehiclesDto[] data = entries;
		if (minId.HasValue && maxId.HasValue)
		{
			data =
				entries.Where(i => i.Id >= minId.Value && i.Id <= maxId.Value)
				.ToArray<DriverVehiclesDto>();
		}
		else if (minId.HasValue)
		{
			data =
				entries.Where(i => i.Id >= minId)
				.ToArray<DriverVehiclesDto>();
		}
		else if (maxId.HasValue)
		{
			data =
				entries.Where(i => i.Id <= maxId)
				.ToArray<DriverVehiclesDto>();
		}

		if (data?.Length == 0)
			return null;

		// Validate brand ids.
		IEnumerable<VehicleInfoDto> vehicles = entries.SelectMany(i => i.VehicleInfo);
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
		foreach (DriverVehiclesDto i in entries)
			i.Driver.Id = i.Id;

		// Calculate statistics.
		return new()
		{
			AverageAge = GetAverageAge(data!),
			DriversByBirthYear = GetDriversByBirthYear(data!),
			DriversByLastName = GetDriversByLastName(data!),
			MostFrequentBrandNames = GetMostFrequentBrandNames(data!),
			AverageVehicleAgeByBrandName = GetAverageVehicleAgeByBrandName(data!),
			EngineTypePercentage = GetEngineTypePercentage(data!),
			BlueEyedDriversWithHybridOrElectricsVehicles = GetBlueEyedDrivers(data!),
			DriversWithSameEngineType = GetDriversWithSameEngineType(data!)
		};
	}

	/// <summary>
	/// Returns ids of drivers that have multiple vehicles and whose vehicles have all the same engine type.
	/// </summary>
	/// <param name="entries">Array of DriverVehicles DTOs</param>
	/// <returns>Ids of drivers that have multiple vehicles and whose vehicles have all the same engine type.</returns>
	private int[] GetDriversWithSameEngineType(DriverVehiclesDto[] entries)
	{
		List<int> result = new();

		// Query drivers with multiple vehicles.
		IEnumerable<DriverVehiclesDto> drivers = entries.Where(d => d.VehicleInfo.Count() > 1);
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
	/// <param name="entries">Array of DriverVehicles DTOs</param>
	/// <returns>Ids of blue-eyed drivers using wehicles with hybrid or electric engines.</returns>
	private int[] GetBlueEyedDrivers(DriverVehiclesDto[] entries)
	{
		return
					entries.Where(i => i.Driver.EyeColor == "blue")
					.Where(i => i.VehicleInfo.Any(v => v.EngineType == EngineType.Electric || v.EngineType == EngineType.Hybrid))
					.Select(i => i.Driver.Id)
					.ToArray<int>();
	}

	/// <summary>
	/// Returns percentage of drivers driving vehicles with a specific engine type.
	/// </summary>
	/// <param name="entries">Array of DriverVehicles DTOs</param>
	/// <returns>Percentage of drivers driving vehicles with a specific engine type.</returns>
	private Dictionary<EngineType, double> GetEngineTypePercentage(DriverVehiclesDto[] entries)
	{
		Dictionary<EngineType, double> result = new();

		foreach (EngineType engine in Enum.GetValues(typeof(EngineType)))
		{
			double engineCount =
							entries
							.Where(i => i.VehicleInfo.Any(v => v.EngineType == engine))
							.Count();

			result[engine] = engineCount / ((double)entries.Length / 100);
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
	/// <param name="entries">Array of DriverVehicles DTOs</param>
	/// <returns>The average age of the vehicles by brand name.</returns>
	private Dictionary<string, int>? GetAverageVehicleAgeByBrandName(DriverVehiclesDto[] entries)
	{
		// Get all brands.
		IEnumerable<string> brandnames =
		entries.SelectMany(i => i!.VehicleInfo!)!
			.Select(v => v!.Brand!.Name!)
			.Distinct();

		// All vehicles.
		var vehicles = entries.SelectMany(i => i.VehicleInfo);

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
	/// <param name="entries">Array of DriverVehicles DTOs</param>
	/// <returns>A string array with three most frequent brand names.</returns>
	private string[]? GetMostFrequentBrandNames(DriverVehiclesDto[] entries)
	{
		IEnumerable<VehicleInfoDto> vehicles =
			entries.SelectMany(i => i.VehicleInfo);

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
	/// <param name="entries">Array of DriverVehicles DTOs</param>
	/// <returns>Drivers grouped by last name.</returns>
	private Dictionary<string, int[]> GetDriversByLastName(DriverVehiclesDto[] entries)
	{
		var groups =
			entries.Select(i => i.Driver)
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
	/// <param name="entries">Array of DriverVehicles DTOs</param>
	/// <returnsDrivers grouped by birth year.></returns>
	private Dictionary<int, int> GetDriversByBirthYear(DriverVehiclesDto[] entries)
	{
		var groups =
		entries.Select(i => i.Driver)
		.GroupBy(d => d.BirthDate.Year)
		.OrderBy(g => g.Key);

		return
			groups.Select(g => new { key = g.Key, count = g.Count() })
		.ToDictionary(g => g.key, g => g.count);
	}

	/// <summary>
	/// Gets the average age of the drivers.
	/// </summary>
	/// <param name="entries">DTO array</param>
	/// <returns>Rounded average age</returns>
	private int GetAverageAge(DriverVehiclesDto[] entries)
	{
		double averageAge = entries.Average(i => GetAge(i.Driver.BirthDate));
		return (int)Math.Round(averageAge);
	}
}
