namespace Garage.Data.Interfaces;

/// <summary>
/// defines CRUD operations for a repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseRepository<TEntity> where TEntity : class
{
	/// <summary>
	/// Returns all records in the repository.
	/// </summary>
	/// <returns>IList with records</returns>
	IList<TEntity> GetAll();

	/// <summary>
	/// Finds a record by its id.
	/// </summary>
	/// <param name="id">Id of the wanted record</param>
	/// <returns>The record or null</returns>
	TEntity? FindById(int id);

	/// <summary>
	/// Inserts a new record into the repository.
	/// </summary>
	/// <param name="entity">The record to be inserted</param>
	/// <returns>The inserted record</returns>
	TEntity Insert(TEntity entity);

	/// <summary>
	/// Deletes a record from the repository.
	/// </summary>
	/// <param name="id">Id of the record to be deleted</param>
	void Delete(int id);

	/// <summary>
	/// Checks if a record with the given id exists in the repository.
	/// </summary>
	/// <param name="id">Id of the record</param>
	/// <returns>True if the specified record exists in the repository</returns>
	bool ExistsWithId(int id);
}
