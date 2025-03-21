using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rotas.Domain.Interfaces
{
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
        Task<List<T>> SearchFilteredAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Get an entity by ID
        /// </summary>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Create a new entity and return the ID generated
        /// </summary>
        Task<int> CreateAsync(T entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Delete an entity by ID
        /// </summary>
        Task DeleteAsync(int id);
    }
}
