using AutoMapper;

using Garage.Business.Models;
using Garage.Data.Models;

namespace Garage;

/// <summary>
///  A class for automapper configuration
/// </summary>
public class AutomapperConfigurationProfile : Profile
{
	/// <summary>
	/// Constructor
	/// </summary>
	public AutomapperConfigurationProfile()
	{
		CreateMap<Brand, BrandDto>();
		CreateMap<BrandDto, Brand>();

		CreateMap<Vehicle, VehicleInfoDto>();
		CreateMap<VehicleInfoDto, Vehicle>();

		CreateMap<Driver, DriverDto>();
		CreateMap<DriverDto, Driver>();

		CreateMap<DriverVehicles, DriverVehiclesDto>();
		CreateMap<DriverVehiclesDto, DriverVehicles>();
	}
}
