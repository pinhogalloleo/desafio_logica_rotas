
using System.Linq.Expressions;

namespace Rotas.Domain.Interfaces;
public interface IRepositoryCrud<T> where T : class
{
    /// <summary>
    /// Get all entities
    /// </summary>
    Task<List<T>> GetAllAsync();

    /// <summary>
    /// Get all entities filtered by a expression, like "x => x.Nome=="alfredo".
    /// </summary>
    /// <example>
    /// <code>
    /// var result = await repository.SearchFilteredAsync(x => x.Nome == "alfredo");
    /// </code>
    /// </example>
    Task<List<T>> SearchByExpressionAsync(Expression<Func<T, bool>> filter);

    /// <summary>
    /// Get an entity by ID
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Create a new entity and return the ID generated
    /// </summary>
    Task<int> InsertAsync(T entity);

    /// <summary>
    /// Update an entity
    /// </summary>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Delete an entity by its ID
    /// </summary>
    Task DeleteAsync(int id);
}
