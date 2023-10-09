using System.Data;

namespace Rubic.EntityFramework.Repositories.Abstracts;

/// <summary>
/// Repository
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    void Insert(T entity);

    /// <summary>
    /// Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    Task InsertAsync(T entity);

    /// <summary>
    /// Delete an item from the repoistory
    /// </summary>
    /// <typeparam name="T">The type of the entity</typeparam>
    /// <param name="entity">The entity to delete</param>
    void Delete(T entity);

    /// <summary>
    /// Commit the changes to the repository
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Indicates whether is called after the changes
    /// have been sent successfully to the database.</param>
    /// <returns>Task</returns>
    void SaveAll(bool acceptAllChangesOnSuccess = true);

    /// <summary>
    /// Commit the changes to the repository
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Indicates whether is called after the changes
    /// have been sent successfully to the database.</param>
    /// <returns>Task</returns>
    Task SaveAllAsync(bool acceptAllChangesOnSuccess = true);

    /// <summary>
    /// Gets a table
    /// </summary>
    IQueryable<T> Table { get; }

    /// <summary>
    /// Execute Command
    /// </summary>
    /// <param name="command">Command string. SQL query.</param>
    /// <returns>The string list of results.</returns>
    IEnumerable<string> ExecuteQuery(string command);

    /// <summary>
    /// Execute Command
    /// </summary>
    /// <param name="command">Command string. SQL query.</param>
    /// <returns>The string list of results.</returns>
    Task<IEnumerable<string>> ExecuteQueryAsync(string command);
}
