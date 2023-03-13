using AutoMapper;

using Garage.Business.Interfaces;
using Garage.Business.Models;
using Garage.Data.Interfaces;
using Garage.Data.Models;

namespace Garage.Business.Managers;

/// <summary>
///  A brand manager.
/// </summary>
public class BrandManager : IBrandManager
{
	/// <summary>
	/// Deletes a brand.
	/// </summary>
	/// <param name="id">Id of the brand to be deleted</param>
	/// <returns>True if the brand was deleted</returns>
	public bool DeleteBrand(int id)
	{
		try
		{
			_brandRepository.Delete(id);
		}
		catch
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Adds a brand.
	/// </summary>
	/// <param name="name">Name of the brand</param>
	/// <returns>A DTO object storing the newly added brand</returns>
	public BrandDto? AddBrand(string name)
	{
		// Store the brand.
		BrandDto brand = new BrandDto { Name = name };

		return AddBrand(brand);
	}


	/// <summary>
	///  Adds a brand.
	/// </summary>
	/// <param name="brandDto">The brand as an DTO object to be added</param>
	/// <returns>Newly added brand as an DTO object</returns>
	public BrandDto AddBrand(BrandDto brandDto)
	{
		Brand brand = _mapper.Map<Brand>(brandDto);
		Brand newBrand = _brandRepository.Insert(brand);

		return _mapper.Map<BrandDto>(newBrand);
	}

	/// <summary>
	///  Returns all brands.
	/// </summary>
	/// <returns>A list of brands</returns>
	public IList<BrandDto>? GetAllBrands()
	{
		IList<Brand> brands = _brandRepository.GetAll();
		return _mapper.Map<IList<BrandDto>>(brands);
	}

	/// <summary>
	/// Finds a brand by its code.
	/// </summary>
	/// <param name="name">Name of the requested brand</param>
	/// <returns>DTO object of the wanted brand or null</returns>
	public BrandDto? GetBrandByName(string name)
	{
		Brand? brand = _brandRepository.FindByName(name);

		if (brand is null)
			return null;

		return _mapper.Map<BrandDto>(brand);
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="brandRepository">The brand repository</param>
	/// <param name="mapper">The mapper</param>
	public BrandManager(IBrandRepository brandRepository, IMapper mapper)
	{
		_brandRepository = brandRepository;
		_mapper = mapper;
	}

	/// <summary>
	///  Gets a brand.
	/// </summary>
	/// <param name="id">Id of the branded brand</param>
	/// <returns>A data transformation object</returns>
	public BrandDto? GetBrand(int id)
	{
		Brand? brand = _brandRepository.FindById(id);

		if (brand is null)
			return null;

		return _mapper.Map<BrandDto>(brand);
	}

	/// <summary>
	///  The brand repository.
	/// </summary>
	private readonly IBrandRepository _brandRepository;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper;
}