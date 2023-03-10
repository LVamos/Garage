namespace Garage.Business;

/// <summary>
/// The exception that is thrown when some invalid brand IDs were entered.
/// </summary>
[Serializable]
public class BrandsNotFoundException : Exception
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="message">A message describing the exception</param>
	/// <param name="invalidBrandIds">Ids of the invalid brands</param>
	public BrandsNotFoundException(string message, int[] invalidBrandIds) : base(message) => InvalidBrandIds = invalidBrandIds;

	/// <summary>
	/// An int arra containing the ids of the invalid brands.
	/// </summary>
	public readonly int[] InvalidBrandIds;
}
