using Garage.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Garage.Data.Repositories;

/// <summary>
/// The base class of all repositories
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
	/// <summary>
	/// Context of a database to work with
	/// </summary>
	protected readonly GarageDbContext _dbContext;

	/// <summary>
	/// Data source for a repository
	/// </summary>
	protected readonly DbSet<TEntity> _dbSet;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="dbContext">Context of a database to work with</param>
	public BaseRepository(GarageDbContext dbContext)
	{
		_dbContext = dbContext;
		_dbSet = dbContext.Set<TEntity>();
	}

	/// <summary>
	/// Finds a record by its id.
	/// </summary>
	/// <param name="id">Id of the wanted record</param>
	/// <returns>The record or null</returns>
	public TEntity? FindById(int id) => _dbSet.Find(id);

	/// <summary>
	/// Checks if a record with the given id exists in the repository.
	/// </summary>
	/// <param name="id">Id of the record</param>
	/// <returns>True if the specified record exists in the repository</returns>
	public bool ExistsWithId(int id)
	{
		TEntity? entity = _dbSet.Find(id);

		// Prevent EF from tracking the entity
		if (entity is not null)
			_dbContext.Entry(entity).State = EntityState.Detached;
		return entity is not null;
	}

	/// <summary>
	/// Returns all records in the repository.
	/// </summary>
	/// <returns>IList with records</returns>
	public IList<TEntity> GetAll() => _dbSet.ToList();

	/// <summary>
	/// Inserts a new record into the repository.
	/// </summary>
	/// <param name="entity">The record to be inserted</param>
	/// <returns>The inserted record</returns>
	public virtual TEntity Insert(TEntity entity)
	{
		EntityEntry<TEntity> entry = _dbSet.Add(entity);
		_dbContext.SaveChanges();
		return entry.Entity;
	}

	/// <summary>
	/// Deletes a record from the repository.
	/// </summary>
	/// <param name="id">Id of the record to be deleted</param>
	public void Delete(int id)
	{
		TEntity? entity = _dbSet.Find(id);

		if (entity is null)
			return;

		try
		{
			_dbSet.Attach(entity);
			_dbSet.Remove(entity);
			_dbContext.SaveChanges();
		}
		catch
		{
			_dbContext.Entry(entity).State = EntityState.Unchanged;
			throw;
		}
	}
}
